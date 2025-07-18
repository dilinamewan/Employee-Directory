using Employee_Directory.Models;
using System.ComponentModel.DataAnnotations;

namespace Employee_Directory.Models.ViewModels
{
    public class EmployeeIndexViewModel
    {
        public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();

        [Display(Name = "Search by Name or Department")]
        public string? SearchTerm { get; set; }

        // Pagination properties
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages { get; set; }
        public int TotalEmployees { get; set; }

        // Helper properties for pagination
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public int StartEmployee => (CurrentPage - 1) * PageSize + 1;
        public int EndEmployee => Math.Min(CurrentPage * PageSize, TotalEmployees);
    }
}
