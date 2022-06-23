using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MvcClient.Models;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace MvcClient.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        { 
            return View();
        }
        [Authorize]
        public async Task<IActionResult> ResourceApi1()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5900/");
            if (disco.IsError)
            {
                throw new Exception(disco.Error);
            }
            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            client.SetBearerToken(accessToken);

            //var response2 = await client.GetAsync("http://localhost:5901/WeatherForecast");
            //var content222 = await response2.Content.ReadAsStringAsync();

            //var response3 = await client.GetAsync(disco.UserInfoEndpoint);
            //var content223 = await response3.Content.ReadAsStringAsync();

            var response = await client.GetAsync("http://localhost:5901/identity");     
            //if (!response.IsSuccessStatusCode)
            //{
            //    if (response.StatusCode == HttpStatusCode.Unauthorized)
            //    {
            //        await RenewTokensAsync();
            //        return RedirectToAction();
            //    }
            //    throw new Exception(response.ReasonPhrase);
            //}
            var content = await response.Content.ReadAsStringAsync();
            ViewData["content"] = content;
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Privacy()
        {
            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            var idToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
            // var code = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.Code);

            ViewData["accessToken"] = accessToken;
            ViewData["idToken"] = idToken;
            ViewData["refreshToken"] = refreshToken;
            return View();
        } 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return RedirectToAction("AccessDenied", "Authorization");
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync("oidc");
            //return SignOut("Cookies", "oidc");
        }
        /// <summary>
        /// 撤销token
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IActionResult> RevokeToken()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5900/");
            if (disco.IsError)
            {
                throw new Exception(disco.Error);
            }

            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                var revokeAccessTokenResponse = await client.RevokeTokenAsync(new TokenRevocationRequest
                {
                    Address = disco.RevocationEndpoint,
                    ClientId = "CodeClient11",
                    ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0",
                    Token = accessToken
                });

                if (revokeAccessTokenResponse.IsError)
                {
                    throw new Exception("Access Token Revocation Failed: " + revokeAccessTokenResponse.Error);
                }
            }
            return View("Index");
        }
        public async Task<IActionResult> RevokeRefreshToken()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5900/");
            if (disco.IsError)
            {
                throw new Exception(disco.Error);
            }

            var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                var revokeRefreshTokenResponse = await client.RevokeTokenAsync(new TokenRevocationRequest
                {
                    Address = disco.RevocationEndpoint,
                    ClientId = "CodeClient11",
                    ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0",
                    Token = refreshToken
                });

                if (revokeRefreshTokenResponse.IsError)
                {
                    throw new Exception("Refresh Token Revocation Failed: " + revokeRefreshTokenResponse.Error);
                }
            }
            return View("Index");
        }
        public async Task Logout2()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5900/");
            if (disco.IsError)
            {
                throw new Exception(disco.Error);
            }

            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                var revokeAccessTokenResponse = await client.RevokeTokenAsync(new TokenRevocationRequest
                {
                    Address = disco.RevocationEndpoint,
                    ClientId = "CodeClient11",
                    ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0",
                    Token = accessToken
                });

                if (revokeAccessTokenResponse.IsError)
                {
                    throw new Exception("Access Token Revocation Failed: " + revokeAccessTokenResponse.Error);
                }
            }

            var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                var revokeRefreshTokenResponse = await client.RevokeTokenAsync(new TokenRevocationRequest
                {
                    Address = disco.RevocationEndpoint,
                    ClientId = "CodeClient11",
                    ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0",
                    Token = refreshToken
                });

                if (revokeRefreshTokenResponse.IsError)
                {
                    throw new Exception("Refresh Token Revocation Failed: " + revokeRefreshTokenResponse.Error);
                }
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync("oidc");
        }
        [HttpGet]
        public async Task<IActionResult> AccessApis()
        {
            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            var apiClient = new HttpClient();
            apiClient.SetBearerToken(accessToken);

            var response1 = await apiClient.GetAsync("http://localhost:5901/identity");
            if (!response1.IsSuccessStatusCode)
            {
                throw new Exception("Access Api1 Failed");
            }

            var api1Result = await response1.Content.ReadAsStringAsync();
            ViewData["api1"] = api1Result;

            var apiClient2 = new HttpClient();
            apiClient2.SetBearerToken(accessToken);

            var response2 = await apiClient2.GetAsync("http://localhost:5902/me");
            if (!response2.IsSuccessStatusCode)
            {
                throw new Exception("Access Api2 Failed");
            }

            var api2Result = await response2.Content.ReadAsStringAsync();
            ViewData["api2"] = api2Result;

            return View();
        }
        /// <summary>
        /// 刷新token
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<string> RenewTokensAsync()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5900");
            if (disco.IsError)
            {
                throw new Exception(disco.Error);
            } 
            var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            // Refresh Access Token
            var tokenResponse = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "CodeClient11",
                ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0",
                Scope = "api1 openid profile email phone address",
                GrantType = OpenIdConnectGrantTypes.RefreshToken,
                RefreshToken = refreshToken
            });

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            var expiresAt = DateTime.UtcNow + TimeSpan.FromSeconds(tokenResponse.ExpiresIn);

            var tokens = new[]
            {
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.IdToken,
                    Value = tokenResponse.IdentityToken
                },
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.AccessToken,
                    Value = tokenResponse.AccessToken
                },
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.RefreshToken,
                    Value = tokenResponse.RefreshToken
                },
                new AuthenticationToken
                {
                    Name = "expires_at",
                    Value = expiresAt.ToString("o", CultureInfo.InvariantCulture)
                }
            };

            // 获取身份认证的结果，包含当前的pricipal和properties
            var currentAuthenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme)!;

            // 把新的tokens存起来
            currentAuthenticateResult.Properties!.StoreTokens(tokens);

            // 登录
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, currentAuthenticateResult.Principal!, currentAuthenticateResult.Properties);

            return tokenResponse.AccessToken;
        }
    }
}