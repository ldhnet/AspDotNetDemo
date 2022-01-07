using Framework.Utility;
using Framework.Utility.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace WebApi6_0.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AddAuthenticationExtension(this IServiceCollection services, JWTTokenOptions tokenOptions)
        {
            //第二步：增加鉴权逻辑
            //第三步：明确那些Api需要支持授权，标记api授权 特性
             
            //配置鉴权流程
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //JWT 有一些默认属性，就是给鉴权时 就可以筛选了
                        ValidateIssuer = true,//是否验证Issuer
                        ValidateAudience = true,
                        ValidateLifetime = true,//是否验证失效时间
                        ValidateIssuerSigningKey = true,
                        ValidAudience = tokenOptions.Audience,
                        ValidIssuer = tokenOptions.Issuer,//这两项和前面签发的配置一样
                                                          //拿到SecurityKey
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
                        AudienceValidator = (m, n, z) =>
                        {
                            //等于去扩展了下 Audience 的校验规则--鉴权
                            //return m != null && m.FirstOrDefault().Equals(builder.Configuration["Audience"]);
                            return true;
                        },
                        //LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                        //{
                        //    bool bResult = notBefore <= DateTime.Now
                        //       && expires >= DateTime.Now;
                        //    //&&validationParameters //自定义校验
                        //    //return bResult;
                        //    return true;
                        //}
                    };
                    //如果校验不通过，可以给一个事件注册一个动作，这个动作就是指定返回结果；
                    options.Events = new JwtBearerEvents
                    {
                        //此处为权限验证失败后出发的事件
                        OnChallenge = context =>
                          {
                              //此处为代码终止 netcore 默认返回类型数据的结果，这个很重要
                              context.HandleResponse();
                              //自定义自己想要的返回结果，我这里返回的是Json 对象，通过引用Newtonsoft.json Jon进行转换
                              //自定以数据类型
                              context.Response.ContentType = "application/json";
                              context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                              context.Response.WriteAsync(JsonConvert.SerializeObject(ResultModel.Error("未授权或token已过期,请重新登录.", StatusCodes.Status401Unauthorized))).ConfigureAwait(false); 
                              return Task.FromResult(0);
                          }
                    };
                });
        }
    }
}
