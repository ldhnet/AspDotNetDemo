// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;

namespace IdpDemo
{
    public class TestUsers
    { 
        public static IHostEnvironment _Environment { get; set; }
        public static IConfiguration _Configuration { get; set; }

        public static SigningCredentials CreateSigningCredential2()
        {
            var signinkey = new RsaSecurityKey(RSA.Create());
            signinkey.KeyId = "abcdefghijklmnopqrstuvwxyz";//How to generate KeyId ??
            var credentials = new SigningCredentials(signinkey,SecurityAlgorithms.RsaSha256); 
            return credentials;
        }
        public  SigningCredentials CreateSigningCredential()
        {
            var signinkey = GetSecurityKey();
            signinkey.KeyId = "123456";
            var credentials = new SigningCredentials(signinkey, SecurityAlgorithms.RsaSha256); 
            return credentials;
        } 
        private SecurityKey GetSecurityKey()
        {
            return new RsaSecurityKey(GetRSACryptoServiceProvider());
        }
        private RSACryptoServiceProvider GetRSACryptoServiceProvider()
        {
            return new RSACryptoServiceProvider(2048);
        }

        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser{SubjectId = "818727", Username = "alice", Password = "alice", 
                Claims = 
                {
                    new Claim(JwtClaimTypes.Name, "Alice Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Alice"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                    new Claim(JwtClaimTypes.Role, "管理员")
                }
            },
            new TestUser{SubjectId = "88421113", Username = "bob", Password = "bob", 
                Claims = 
                {
                    new Claim(JwtClaimTypes.Name, "Bob Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Bob"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                    new Claim("location", "somewhere"),
                    new Claim(JwtClaimTypes.Role, "普通用户")
                }
            }
        };
    }
}