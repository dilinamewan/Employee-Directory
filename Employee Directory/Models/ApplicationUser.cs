using Microsoft.AspNetCore.Identity;

namespace Employee_Directory.Models
{
    /// <summary>
    /// Extended user model for ASP.NET Core Identity authentication system.
    /// Inherits from IdentityUser to include built-in authentication properties
    /// (Username, Email, PasswordHash, etc.) while adding application-specific
    /// user information for the Employee Directory system.
    /// 
    /// This model represents system users who can log in and access the application,
    /// which are separate from the Employee records stored in the system.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// User's first name for personalized greetings and identification.
        /// Marked with [PersonalData] attribute for GDPR compliance and data protection.
        /// Used in combination with LastName to create full display names.
        /// </summary>
        [PersonalData]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// User's last name for complete identification.
        /// Marked with [PersonalData] attribute for GDPR compliance and data protection.
        /// Used in combination with FirstName to create full display names.
        /// </summary>
        [PersonalData]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Computed property that combines FirstName and LastName for display purposes.
        /// Automatically formats the full name with proper spacing and handles cases
        /// where either name might be empty. Used primarily in navigation bars and
        /// user interface elements to show who is currently logged in.
        /// 
        /// Returns a trimmed string to handle cases where only one name is provided.
        /// </summary>
        public string DisplayName => $"{FirstName} {LastName}".Trim();
    }
}
