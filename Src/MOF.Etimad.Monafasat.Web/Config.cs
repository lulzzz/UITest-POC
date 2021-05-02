//using AutoMapper.Configuration;
//using IdentityServer4;
//using IdentityServer4.Models;
//using IdentityServer4.Test;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace MOF.Etimad.Monafasat.Web
//{
//    public static class Config
//   {
//      public static IConfiguration _configuration { get; set; }
//      public static IConfigurationRoot _configuration { get; }
//      public static List<TestUser> GetUsers()
//        {
//            return new List<TestUser>
//            {
//                new TestUser
//                {
//                    SubjectId="1",
//                    Username = "Osama",
//                    Password="password",
//                    Claims = new List<Claim>
//                    {
//                        new Claim("given_name", "Osama"),
//                        new Claim("family_name", "Al Ansari"),
//                        new Claim("address", "Al Wadi, Riyadh"),
//                        new Claim("role", "FreeUser")
//                    }
//                },
//                new TestUser
//                {
//                    SubjectId = "2",
//                    Username = "Ahmed",
//                    Password = "password",
//                    Claims = new List<Claim>
//                    {
//                        new Claim("given_name", "Ahmed"),
//                        new Claim("family_name", "Al Ansari"),
//                        new Claim("address", "Al hamra, Jeddah"),
//                        new Claim("role", "PayingUser")
//                    }
//                }
//            };
//        }

//        public static IEnumerable<IdentityResource> GetIdentityResources()
//        {
//            return new List<IdentityResource>
//            {
//                new IdentityResources.OpenId(),
//                new IdentityResources.Profile(),
//                new IdentityResources.Address(),
//                new IdentityResource("roles", "Your role(s)", new List<string>{"role"})
//            };
//        }

//        public static IEnumerable<Client> GetClients()
//        {
//            return new List<Client>
//            {
//                new Client
//                {
//                    ClientName ="Etimad",
//                    ClientId = "mof.etimad",
//                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
//                    AccessTokenType = AccessTokenType.Jwt,
//                    AllowAccessTokensViaBrowser = true,
//                    AllowOfflineAccess = true,
//                    RequireConsent = false,
//                    AlwaysIncludeUserClaimsInIdToken = true,
//                    AlwaysSendClientClaims = true,                    
//                    RedirectUris = new List<string>()
//                    {
//                        _configuration["Authority:AuthorityURL"] + "/signin-oidc"
//                    },
//                    AllowedScopes =
//                    {
//                        IdentityServerConstants.StandardScopes.OpenId,
//                        IdentityServerConstants.StandardScopes.Profile,
//                        IdentityServerConstants.StandardScopes.Address,
//                        "roles"
//                    },
//                    ClientSecrets =
//                    {
//                        new Secret("secret".Sha256())
//                    },                    
//                    PostLogoutRedirectUris =
//                    {
//                        _configuration["Authority:AuthorityURL"] + "/signout-callback-oidc"
//                    }
//                }
//            };
//        }
//    }
//}
