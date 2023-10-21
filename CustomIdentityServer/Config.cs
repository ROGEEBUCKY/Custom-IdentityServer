using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using System.Security.Claims;

namespace CustomIdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
            new ApiScope("scope1"),
            new ApiScope("scope2"),
            new ApiScope("userinfo",new List<string>{ ClaimTypes.Role, ClaimTypes.Name, ClaimTypes.Email,"Designation" })
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "scope1" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedCorsOrigins = { "https://localhost:44385","https://localhost:44392" },
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                  RequirePkce= true,
                RedirectUris = { "https://localhost:44300/signin-oidc","https://oauth.pstmn.io/v1/callback","https://localhost:44373/Login/AuthorizationCodeCallback","https://localhost:44373/Login/TokenCallback", "https://localhost:44385/authentication/login-callback","https://localhost:44392/swagger/oauth2-redirect.html" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc","https://localhost:44385/" },

                AllowOfflineAccess = true,
                AllowedScopes = {  "userinfo",IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess }
            },  // interactive client using code flow + pkce
            new Client
            {
                ClientId = "PasswordClient",
                ClientSecrets = { new Secret("secert".Sha256()) },

                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                RedirectUris = { "https://localhost:44300/signin-oidc","https://oauth.pstmn.io/v1/callback","https://localhost:44373/Login/AuthorizationCodeCallback" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = {"userinfo",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess
                }
            },
            };
    }
}