using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace GeekShopping.IdentifyServer.Configuration
{
	public class IdentityConfiguration
	{
		public const string Admin = "Admin";
		public const string Client = "Client";

		public static IEnumerable<IdentityResource> IdentityResources =>
			new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Email(),
				new IdentityResources.Profile()
			};

		public static IEnumerable<ApiScope> ApiScopes =>
			new List<ApiScope>
			{
				new ApiScope("geek_shopping", "GeekShopping Server"),
				new ApiScope(name: "read", "Read data."),
				new ApiScope(name: "write", "Write data."),
				new ApiScope(name: "delete", "Delete data.")
			};

		public static IEnumerable<Client> Clients =>
			new List<Client>
			{
				new() {
					ClientId = "client",
					ClientSecrets = { new Secret("secret".Sha256())},
					AllowedGrantTypes = GrantTypes.ClientCredentials,
					AllowedScopes = {"read", "write", "profile"}
				},
				new() {
					ClientId = "geek_shopping",
					ClientSecrets = { new Secret("secret".Sha256())},
					AllowedGrantTypes = GrantTypes.Code,
					RedirectUris = { "https://localhost:3000/signin-oidc", },
					PostLogoutRedirectUris = {"https://localhost:3000/signout-callback-oicd"},
					AllowedScopes = new List<string>
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Email,
						IdentityServerConstants.StandardScopes.Profile,
						"geek_shopping"
					}
				}
			};
	}
}
