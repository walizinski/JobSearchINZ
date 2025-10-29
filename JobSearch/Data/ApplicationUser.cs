using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JobSearch.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
		[Required(ErrorMessage = "Name is required.")]
		public string DisplayName { get; set; } = string.Empty;

	}
}
