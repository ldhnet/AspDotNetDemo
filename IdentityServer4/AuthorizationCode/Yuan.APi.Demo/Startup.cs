using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Yuan.APi.Demo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            #region �� ��ids4�� 

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, options =>
               {
                   options.Authority = "http://localhost:5001";
                   options.RequireHttpsMetadata = false;
                   options.ApiName = "api1";
                   options.ApiSecret = "apipwd"; //��ӦApiResources�е���Կ
              });
            services.AddAuthorization();
            #endregion

            #region �� ���Դ���JwtBearer����

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //   .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            //   {
            //       // IdentityServer ��ַ
            //       options.Authority = "http://localhost:5001";
            //       //����Ҫhttps
            //       options.RequireHttpsMetadata = false;
            //       //����Ҫ�� IdentityServer ����� api1 ����һ��
            //       options.Audience = "api1";
            //   });

            #endregion

            #region �� ���Դ���JwtBearer���򣬲������Ȩ����������

            //services.AddAuthentication("Bearer")
            //   .AddJwtBearer("Bearer", options =>
            //   {
            //       options.Authority = "http://localhost:5001";
            //       options.RequireHttpsMetadata = false;
            //       options.TokenValidationParameters = new TokenValidationParameters
            //       {
            //           ValidateAudience = false
            //       };
            //   });

            //// adds an authorization policy to make sure the token is for scope 'api1'
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("ApiScope", policy =>
            //    {
            //        policy.RequireAuthenticatedUser();
            //        policy.RequireClaim("scope", "api1");
            //    });
            //});

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                //endpoints.MapDefaultControllerRoute().RequireAuthorization("ApiScope"); // adds scope
            });
        }
    }
}
