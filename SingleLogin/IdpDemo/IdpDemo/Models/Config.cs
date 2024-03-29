﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdpDemo.Models;

namespace IdpDemo
{
    public static class Config
    {
        //下面是需要新加上方法，否则访问提示invalid_scope
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[] { 
                new ApiScope("api1"),
                new ApiScope("api2")
        };
        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("api1", "My API #1"),
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
            };
        }
         
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            { 
                // mvc client, authorization code
                new Client
                {
                    ClientId = GlobalContext.IdpClients_mvc.ClientId,//"CodeClient",
                    ClientName = GlobalContext.IdpClients_mvc.ClientName,//"ASP.NET Core MVC Client", 
                    AllowedGrantTypes = GrantTypes.Code,      
                    ClientSecrets = { new Secret(GlobalContext.IdpClients_mvc.ClientSecrets.Sha256()) }, //"49C1A7E1-0C79-4A89-A3D6-A37998FB86B0"
                    RedirectUris = { $"{GlobalContext.IdpClients_mvc.RedirectUri}/signin-oidc" }, 
                    FrontChannelLogoutUri = $"{GlobalContext.IdpClients_mvc.RedirectUri}/signout-oidc",
                    PostLogoutRedirectUris = { $"{GlobalContext.IdpClients_mvc.RedirectUri}/signout-callback-oidc" },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequireConsent = false,
                    AllowOfflineAccess = true, // offline_access 允许离线访问  刷新token //如果要获取refresh_tokens ,必须把AllowOfflineAccess设置为true 同事添加 scopes OfflineAccess
                    AccessTokenLifetime = 60 * 3, //  180 秒
                    LogoUri = Path.Combine(GlobalContext._Environment.ContentRootPath,"head.jpg"),
                    AllowedScopes =
                    {
                        "api1", 
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess
                    }
                },
                new Client
                {
                    ClientId = GlobalContext.IdpClients_mvc_test.ClientId,//"CodeClient",
                    ClientName = GlobalContext.IdpClients_mvc_test.ClientName,//"ASP.NET Core MVC Client", 
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = { new Secret(GlobalContext.IdpClients_mvc_test.ClientSecrets.Sha256()) }, //"49C1A7E1-0C79-4A89-A3D6-A37998FB86B0"
                    RedirectUris = { $"{GlobalContext.IdpClients_mvc_test.RedirectUri}/signin-oidc" },
                    FrontChannelLogoutUri = $"{GlobalContext.IdpClients_mvc_test.RedirectUri}/signout-oidc",
                    PostLogoutRedirectUris = { $"{GlobalContext.IdpClients_mvc_test.RedirectUri}/signout-callback-oidc" },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequireConsent = false,
                    AllowOfflineAccess = true, // offline_access 允许离线访问  刷新token //如果要获取refresh_tokens ,必须把AllowOfflineAccess设置为true 同事添加 scopes OfflineAccess
                    AccessTokenLifetime = 60 * 3, // 180 秒 
                    AllowedScopes =
                    {
                        "api1",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess
                    }
                },
                new Client
                {
                    ClientId = "console client",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },//console secret
                    AllowedScopes = {
                        "api1",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess
                    }
                },


            };
        }
    }
}