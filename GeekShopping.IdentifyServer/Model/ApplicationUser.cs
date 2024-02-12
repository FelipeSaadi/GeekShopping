using Microsoft.AspNetCore.Identity;

namespace GeekShopping.IdentifyServer.Model
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
