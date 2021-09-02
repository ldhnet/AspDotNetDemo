using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Code;
using WebApi.Handler;
using WebApi.Middleware; 

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
            //services.AddHealthChecks();
            services.AddHealthChecks().AddDbContextCheck<ApiDbContext>()
                    .AddCheck<ApiHealthCheck>("ApiHealth");
            services.AddControllers(options =>
            {
                //options.FormatterMappings.SetMediaTypeMappingForFormat("json2", MediaTypeHeaderValue.Parse("application/json"));
                //for (int i = 0; i < options.OutputFormatters.Count; i++)
                //{
                //    if (options.OutputFormatters[i] is Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter jsonOutputFormatter)
                //    {
                //        options.OutputFormatters[i] = new CustomJsonOutputFormatter(jsonOutputFormatter.SerializerOptions);
                //        break;
                //    }
                //}
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });

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


            app.UseAuthentication();
            app.UseAuthorization();


       
            app.UseMiddleware(typeof(AuthorizeMiddleware));
            app.UseMiddleware(typeof(OtherMiddleware));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
