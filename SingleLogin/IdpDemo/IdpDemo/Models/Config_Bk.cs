// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;

namespace IdpDemo
{
    public static class Config_bk
    {
        //上面是原来的代码

        //下面是需要新加上方法，否则访问提示invalid_scope
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[] { 
                new ApiScope("api1"),
                new ApiScope("api2")
        };
        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                //new ApiResource("api1", "My API #1"),
        
                new ApiResource("api1", "My API #1", new List<string> { "location" })
                {
                    ApiSecrets = { new Secret("api1 secret".Sha256()) }
                },
                new ApiResource("api2", "Express API")
            };
        }


        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResources.Phone(),
                new IdentityResources.Email(), 
                new IdentityResource("roles", "角色", new List<string> { JwtClaimTypes.Role }),
                new IdentityResource("locations", "地点", new List<string> { "location" }),
            };
        }
         
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "console client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = {IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile }
                },



                // client credentials flow client
                new Client
                {
                    ClientId = "web client",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("web secret".Sha256()) },//511536EF-F270-4058-80CA-1C89C192F69A
                    AllowedScopes = { "api1", IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile }
                },

                // wpf client, password grant
                new Client
                {
                    ClientId = "wpf client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("wpf secrect".Sha256())
                    },
                    AllowedScopes = {
                        "api1",
                        "api2",
                        IdentityServerConstants.StandardScopes.OpenId,//必须要添加，否则报forbidden错误
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Profile }
                },

                // mvc client, authorization code
                new Client
                {
                    ClientId = "interactive",
                    ClientName = "ASP.NET Core MVC Client",

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
      
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    RedirectUris = { "http://localhost:5902/signin-oidc" },

                    FrontChannelLogoutUri = "http://localhost:5902/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:5902/signout-callback-oidc" },

                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequireConsent = false,
                    AllowOfflineAccess = true, // offline_access 允许离线访问  刷新token
                    AccessTokenLifetime = 60, // 60 seconds

                    AllowedScopes =
                    {
                        "api1",
                        "api2",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
#region implicit隐式流客户端
                // OpenID Connect隐式流客户端（MVC）
                new Client
                {
                    ClientId = "implicit-clientid",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,//隐式方式
            
                    RedirectUris = { "http://localhost:5902/signin-oidc" },//登录成功后返回的客户端地址
                    PostLogoutRedirectUris = { "http://localhost:5902/signout-callback-oidc" },//注销登录后返回的客户端地址
                    
                    ClientUri = "http://localhost:5902",
                    AllowedCorsOrigins = { "http://localhost:5902" },

                    RequireConsent=true,//如果不需要显示否同意授权 页面 这里就设置为false
                    AccessTokenLifetime = 60 * 5,
                    AllowedScopes = {"openid", "profile" }
                },
                // angular, implicit flow
                new Client
                {
                    ClientId = "angular-client",
                    ClientName =  "Angular SPA 客户端",
                    ClientUri = "http://localhost:5902",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    AccessTokenLifetime = 60 * 5,

                    RedirectUris =
                    {
                        "http://localhost:5902/signin-oidc",
                        "http://localhost:5902/redirect-silentrenew"
                    },

                    PostLogoutRedirectUris =
                    {
                        "http://localhost:5902"
                    },

                    AllowedCorsOrigins =
                    {
                        "http://localhost:5902"
                    },
                    AllowedScopes = { "openid", "profile" }
                },
#endregion
                // mvc, hybrid flow
                new Client
                {
                    ClientId = "hybrid client",
                    ClientName = "ASP.NET Core Hybrid 客户端",
                    ClientSecrets = {new Secret("hybrid secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AccessTokenType = AccessTokenType.Reference,
                    RequireConsent = false,
                    RequirePkce = false,//一定要设置为false 否则会报错 code challenge Required

                    RedirectUris =
                    {
                        "http://localhost:7000/signin-oidc"
                    },

                    PostLogoutRedirectUris =
                    {
                        "http://localhost:7000/signout-callback-oidc"
                    },

                    AllowOfflineAccess = true,

                    AlwaysIncludeUserClaimsInIdToken = true,

                    AllowedScopes =
                    {
                        "api1","api2",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "locations"
                    }
                },

                // Python Flask client using authorization code flow
                new Client
                {
                    ClientId = "flask client",
                    ClientName = "Flask 客户端",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = { new Secret("flask secret".Sha256()) },
                    Enabled = true,
                    RequireConsent = false,
                    AllowRememberConsent = false,
                    AccessTokenType = AccessTokenType.Jwt,
                    AlwaysIncludeUserClaimsInIdToken = false,
                    AllowOfflineAccess = true,

                    RedirectUris = { "http://localhost:7002/oidc_callback" },

                    AllowedScopes =
                    {
                        "api1",
                        "api2",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }
    }
}