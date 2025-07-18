using Microsoft.AspNetCore.Identity;

namespace Employee_Directory.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; } = string.Empty;

        [PersonalData]
        public string LastName { get; set; } = string.Empty;

        // Display name for the navigation bar
        public string DisplayName => $"{FirstName} {LastName}".Trim();
    }
}
