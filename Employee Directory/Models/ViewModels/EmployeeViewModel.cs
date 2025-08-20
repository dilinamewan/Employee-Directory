using Employee_Directory.Models;
using System.ComponentModel.DataAnnotations;

namespace Employee_Directory.Models.ViewModels
{
    /// <summary>
    /// View model for the Employee Index page that handles employee listing,
    /// search functionality, and pagination. This model consolidates all data
    /// needed to display a paginated and searchable list of employees.
    /// 
    /// This approach separates the view concerns from the domain model,
    /// allowing for optimized data transfer and better maintainability.
    /// </summary>
    public class EmployeeIndexViewModel
    {
        /// <summary>
        /// Collection of employees to display on the current page.
        /// This is a subset of all employees based on pagination and search filters.
        /// </summary>
        public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();

        /// <summary>
        /// The current search term entered by the user.
        /// Used to filter employees by name or department.
        /// Preserved across page refreshes and pagination to maintain user context.
        /// </summary>
        [Display(Name = "Search by Name or Department")]
        public string? SearchTerm { get; set; }

        // =================================================================
        // Pagination Properties
        // These properties work together to provide a complete pagination system
        // =================================================================

        /// <summary>
        /// The current page number being displayed (1-based indexing).
        /// Used for pagination controls and calculating which records to show.
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// Number of employees to display per page.
        /// Allows for adjustable page sizes while maintaining consistent user experience.
        /// Default is 10 employees per page for optimal readability.
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Total number of pages available based on the current search filter.
        /// Calculated by dividing total employees by page size and rounding up.
        /// Used for generating pagination navigation controls.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Total number of employees matching the current search criteria.
        /// This count reflects the filtered results, not the entire employee database.
        /// Used for displaying result counts and calculating pagination.
        /// </summary>
        public int TotalEmployees { get; set; }

        // =================================================================
        // Helper Properties for Pagination UI
        // These computed properties simplify the view logic
        // =================================================================

        /// <summary>
        /// Indicates whether there is a previous page available.
        /// Used to enable/disable the "Previous" button in pagination controls.
        /// </summary>
        public bool HasPreviousPage => CurrentPage > 1;

        /// <summary>
        /// Indicates whether there is a next page available.
        /// Used to enable/disable the "Next" button in pagination controls.
        /// </summary>
        public bool HasNextPage => CurrentPage < TotalPages;

        /// <summary>
        /// The employee number of the first employee shown on the current page.
        /// Used for displaying "Showing X-Y of Z employees" information.
        /// Example: If on page 2 with page size 10, this would be 11.
        /// </summary>
        public int StartEmployee => (CurrentPage - 1) * PageSize + 1;

        /// <summary>
        /// The employee number of the last employee shown on the current page.
        /// Accounts for the last page potentially having fewer employees than the page size.
        /// Example: If on last page with 3 employees out of page size 10, this shows 3, not 10.
        /// </summary>
        public int EndEmployee => Math.Min(CurrentPage * PageSize, TotalEmployees);
    }
}
