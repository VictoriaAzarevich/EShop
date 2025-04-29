using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using System.Security.Claims;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {
                new ApiScope(name: "productAPI", displayName: "Product API")
            };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource("productAPI")
            {
                Scopes = { "productAPI" },
                UserClaims = { "role" }  
            }
        };

    public static IEnumerable<Client> Clients =>
        [
        new Client
            {
                ClientId = "productClient",
                ClientSecrets = { new Secret("productSecret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "productAPI" }
            },
            new Client
            {
                ClientId = "swaggerClient",
                ClientSecrets = { new Secret("swaggerSecret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:5002/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                AllowedScopes = { "openid", "profile", "email", "productAPI" },
                RequirePkce = true,
                AllowAccessTokensViaBrowser = true
            }
        ];

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles", "User roles", new List<string> { "role" }) 
        };

    public static List<TestUser> TestUsers =>
        new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "admin",
                Password = "password",
                Claims = new List<Claim>
                {
                    new Claim("role", "admin"), 
                    new Claim("scope", "productAPI") 
                }
            },
            new TestUser
            {
                SubjectId = "2",
                Username = "user",
                Password = "password",
                Claims = new List<Claim>
                {
                    new Claim("role", "user"), 
                    new Claim("scope", "productAPI") 
                }
            }
        };
}