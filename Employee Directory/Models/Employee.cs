using System.ComponentModel.DataAnnotations;

namespace Employee_Directory.Models
{
    /// <summary>
    /// Represents an employee entity in the Employee Directory system.
    /// Contains all personal and professional information about an employee,
    /// including validation rules and computed properties.
    /// 
    /// This model is used throughout the application for data persistence,
    /// form binding, and display purposes. All properties include appropriate
    /// validation attributes to ensure data integrity.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Unique identifier for the employee record.
        /// Auto-generated primary key used for database operations and references.
        /// </summary>
        [Key]
        public int EmployeeId { get; set; }

        /// <summary>
        /// Employee's complete name (first name and last name combined).
        /// Required field with maximum length of 100 characters.
        /// Used for display purposes and search functionality.
        /// </summary>
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Employee's business email address.
        /// Must be unique across all employees and follow standard email format.
        /// Used for communication and login purposes.
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Employee's job title or role within the organization.
        /// Required field that describes the employee's current position.
        /// Examples: "Software Engineer", "Project Manager", "HR Specialist"
        /// </summary>
        [Required(ErrorMessage = "Position is required")]
        [StringLength(50, ErrorMessage = "Position cannot exceed 50 characters")]
        public string Position { get; set; } = string.Empty;

        /// <summary>
        /// Department or division where the employee works.
        /// Used for organizational structure and filtering/reporting purposes.
        /// Examples: "IT", "Human Resources", "Finance", "Marketing"
        /// </summary>
        [Required(ErrorMessage = "Department is required")]
        [StringLength(50, ErrorMessage = "Department cannot exceed 50 characters")]
        public string Department { get; set; } = string.Empty;

        /// <summary>
        /// Employee's business phone number.
        /// Must be exactly 10 digits without any formatting characters.
        /// Used for direct communication with the employee.
        /// 
        /// Format: 1234567890 (no dashes, spaces, or parentheses)
        /// The regex validation ensures only numeric input is accepted.
        /// </summary>
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must contain exactly 10 digits")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Date when the employee was hired by the organization.
        /// Required field used for calculating tenure and other HR metrics.
        /// Automatically formatted as a date input in forms.
        /// </summary>
        [Required(ErrorMessage = "Hire Date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        /// <summary>
        /// Calculated property that returns the employee's years of service.
        /// Automatically computed based on the difference between the current date
        /// and the hire date. This is a read-only property used for display purposes.
        /// 
        /// Note: This calculation uses simple year difference and doesn't account
        /// for exact dates within the year (e.g., someone hired in December 2023
        /// would show 1 year of service in January 2024).
        /// </summary>
        [Display(Name = "Years of Service")]
        public int YearsOfService => DateTime.Now.Year - HireDate.Year;
    }
}
