using Duende.IdentityServer.Models;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("roles", "Your role(s)", new[] { "role" }),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("product_api", "Product API"),
                new ApiScope("coupon_api", "Coupon API"),
                new ApiScope("cart_api", "Cart API")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "coupon-api-client",
                    ClientName = "Coupon API Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("coupon-api-secret".Sha256()) },
                    AllowedScopes = { "coupon_api" }
                },
                new Client
                {
                    ClientId = "product-api-client",
                    ClientName = "Product API Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("product-api-secret".Sha256()) },
                    AllowedScopes = { "product_api" }
                },
                new Client
                {
                    ClientId = "cart-api-client",
                    ClientName = "Cart API Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("cart-api-secret".Sha256()) },
                    AllowedScopes = { "cart_api" }
                },
                new Client
                {
                    ClientId = "react-client",
                    ClientName = "React SPA",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = { "http://localhost:3000/callback" },
                    PostLogoutRedirectUris = { "http://localhost:3000" },
                    AllowedCorsOrigins = { "http://localhost:3000" },

                    AllowedScopes = { "openid", "profile", "role", "api1" },
                    AllowAccessTokensViaBrowser = true
                },
            };
    }
}
