using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Code;
using WebApi.Extension;
using WebApi.Handler;
using WebApi.Middleware;
using WebApi.NLog;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            //services.AddHealthChecks().AddDbContextCheck<ApiDbContext>()
            //        .AddCheck<ApiHealthCheck>("ApiHealth");

            services.AddControllers(options => { });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });
            services.AddHttpContextAccessor();

            services.AddSingleton<INLogHelper, NLogHelper>();
   
            services.AddAuthentication(options =>
            {
                options.AddScheme<AuthenticateHandler>(AuthenticateHandler.SchemeName, "default scheme");
                options.DefaultAuthenticateScheme = AuthenticateHandler.SchemeName;
                options.DefaultChallengeScheme = AuthenticateHandler.SchemeName;
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }
            app.UseMiddleware(typeof(ExceptionMiddleWare));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseHealthChecks("/health");

            app.UseMiddleware<AdminSafeListMiddleware>("127.0.0.1;192.168.1.5;::1");

            app.UseMiddleware(typeof(OtherMiddleware));
             
            app.UseAuthentication();
            app.UseAuthorization();
             
            app.UseMiddleware(typeof(AuthorizeMiddleware));

            app.UseCalculateExecutionTime();//计算接口执行时间 中间件

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
