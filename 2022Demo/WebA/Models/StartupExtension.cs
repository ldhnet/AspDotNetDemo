namespace WebA.Models
{
    public static class StartupExtension
    { 
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var identityUrl = configuration.GetValue<string>("IdentityUrl");
            var callBackUrl = configuration.GetValue<string>("CallBackUrl");
            var sessionCookieLifetime = configuration.GetValue("SessionCookieLifetimeMinutes", 60);

            // Add Authentication services          

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "Bearer";
            })
            .AddCookie(setup => setup.ExpireTimeSpan = TimeSpan.FromMinutes(sessionCookieLifetime));
            //.AddOpenIdConnect(options =>
            //{
            //    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.Authority = identityUrl.ToString();
            //    options.SignedOutRedirectUri = callBackUrl.ToString();
            //    options.ClientId = "mvc";
            //    options.ClientSecret = "secret";
            //    options.ResponseType = "code id_token";
            //    options.SaveTokens = true;
            //    options.GetClaimsFromUserInfoEndpoint = true;
            //    options.RequireHttpsMetadata = false;
            //    options.Scope.Add("openid");
            //    options.Scope.Add("profile");
            //    options.Scope.Add("orders");
            //    options.Scope.Add("basket");
            //    options.Scope.Add("webshoppingagg");
            //    options.Scope.Add("orders.signalrhub");
            //});

            return services;
        }



    }
}
