using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Employee_Directory.Data;
using Employee_Directory.Models;
using Employee_Directory.Models.ViewModels;

namespace Employee_Directory.Controllers
{
    /// <summary>
    /// Controller responsible for managing employee-related operations in the Employee Directory.
    /// All actions require user authentication to ensure data security and access control.
    /// 
    /// This controller handles CRUD operations (Create, Read, Update, Delete) for employee records,
    /// including search functionality and pagination for better performance with large datasets.
    /// </summary>
    [Authorize] // Require authentication for all actions in this controller
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeeController> _logger;

        /// <summary>
        /// Initializes a new instance of the EmployeeController with required dependencies.
        /// </summary>
        /// <param name="context">Database context for data access operations</param>
        /// <param name="logger">Logger for tracking operations and debugging</param>
        public EmployeeController(ApplicationDbContext context, ILogger<EmployeeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Displays a paginated list of employees with optional search functionality.
        /// This is the main page for viewing and searching through employee records.
        /// </summary>
        /// <param name="searchTerm">Optional search term to filter employees by name or department</param>
        /// <param name="page">Current page number for pagination (default: 1)</param>
        /// <param name="pageSize">Number of employees to display per page (default: 10)</param>
        /// <returns>View with EmployeeIndexViewModel containing filtered and paginated employee data</returns>
        // GET: Employee
        public async Task<IActionResult> Index(string? searchTerm, int page = 1, int pageSize = 10)
        {
            // Start with all employees from the database
            var query = _context.Employees.AsQueryable();

            // Apply search filter if a search term was provided
            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Search in both full name and department fields (case-sensitive)
                query = query.Where(e => e.FullName.Contains(searchTerm) ||
                                        e.Department.Contains(searchTerm));
            }

            // Calculate total count for pagination controls
            var totalEmployees = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalEmployees / (double)pageSize);

            // Ensure page number is within valid bounds
            page = Math.Max(1, Math.Min(page, totalPages));

            // Apply pagination and sorting
            var employees = await query
                .OrderBy(e => e.FullName) // Sort alphabetically by full name
                .Skip((page - 1) * pageSize) // Skip records for previous pages
                .Take(pageSize) // Take only records for current page
                .ToListAsync();

            // Create view model with all necessary data for the view
            var viewModel = new EmployeeIndexViewModel
            {
                Employees = employees,
                SearchTerm = searchTerm,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalEmployees = totalEmployees
            };

            return View(viewModel);
        }

        /// <summary>
        /// Displays detailed information for a specific employee.
        /// Shows all employee fields including calculated fields like years of service.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to display</param>
        /// <returns>Employee details view or NotFound if employee doesn't exist</returns>
        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Validate that an ID was provided
            if (id == null)
            {
                return NotFound();
            }

            // Find the employee with the specified ID
            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);

            // Return NotFound if employee doesn't exist
            if (employee == null)
            {
                return NotFound();
            }

            // Return the details view with the employee data
            return View(employee);
        }

        /// <summary>
        /// Displays the form for creating a new employee record.
        /// Returns an empty employee model for the form to bind to.
        /// </summary>
        /// <returns>Create employee form view</returns>
        // GET: Employee/Create
        public IActionResult Create()
        {
            // Return the create form with an empty employee model
            return View();
        }

        /// <summary>
        /// Processes the creation of a new employee record.
        /// Validates the submitted data and saves it to the database if valid.
        /// Includes comprehensive logging for debugging and audit purposes.
        /// </summary>
        /// <param name="employee">The employee model populated from the form submission</param>
        /// <returns>Redirects to Index on success, or returns form with validation errors</returns>
        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against cross-site request forgery attacks
        public async Task<IActionResult> Create(Employee employee)
        {
            // Log the creation attempt for debugging and audit purposes
            _logger.LogInformation("Create POST method called for employee: {EmployeeName}", employee.FullName);
            _logger.LogInformation("ModelState.IsValid: {IsValid}", ModelState.IsValid);

            // Check if model validation failed
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model validation failed for employee creation");
                
                // Log detailed validation errors for debugging
                foreach (var error in ModelState)
                {
                    _logger.LogWarning("Key: {Key}, Errors: {Errors}", 
                        error.Key, string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage)));
                }
                
                // Return the form with validation errors displayed
                return View(employee);
            }

            // Proceed with database operations if validation passed
            if (ModelState.IsValid)
            {
                try
                {
                    // Add the employee to the database context
                    _context.Add(employee);
                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    // Log successful creation
                    _logger.LogInformation("Employee {EmployeeName} created successfully", employee.FullName);
                    // Set success message for the user
                    TempData["Success"] = "Employee created successfully!";

                    // Redirect to the employee list page
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log any database or system errors
                    _logger.LogError(ex, "Error creating employee {EmployeeName}", employee.FullName);
                    // Add user-friendly error message
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the employee. Please try again.");
                }
            }

            // If we reach here, something went wrong - return the form with errors
            return View(employee);
        }

        /// <summary>
        /// Displays the form for editing an existing employee record.
        /// Populates the form with current employee data.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to edit</param>
        /// <returns>Edit form view with current employee data, or NotFound if employee doesn't exist</returns>
        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Validate that an ID was provided
            if (id == null)
            {
                return NotFound();
            }

            // Find the employee in the database
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            // Return the edit form populated with current employee data
            return View(employee);
        }

        /// <summary>
        /// Processes the update of an existing employee record.
        /// Validates the submitted data, handles concurrency conflicts, and updates the database.
        /// </summary>
        /// <param name="id">The employee ID from the URL route</param>
        /// <param name="employee">The updated employee model from the form</param>
        /// <returns>Redirects to Index on success, or returns form with errors</returns>
        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against cross-site request forgery attacks
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            // Ensure the route ID matches the employee ID (security check)
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            // Proceed only if all validations passed
            if (ModelState.IsValid)
            {
                try
                {
                    // Update the employee in the database context
                    _context.Update(employee);
                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    // Log successful update
                    _logger.LogInformation("Employee {EmployeeName} updated successfully", employee.FullName);
                    // Set success message for the user
                    TempData["Success"] = "Employee updated successfully!";

                    // Redirect to the employee list
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle the case where the record was modified by another user
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound(); // Employee was deleted by another user
                    }
                    else
                    {
                        // Re-throw the exception for unhandled concurrency issues
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    // Log any unexpected errors
                    _logger.LogError(ex, "Error updating employee {EmployeeName}", employee.FullName);
                    // Add user-friendly error message
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the employee. Please try again.");
                }
            }

            // If we reach here, validation failed or an error occurred
            return View(employee);
        }

        /// <summary>
        /// Displays the confirmation page for deleting an employee.
        /// Shows employee details and asks for confirmation before deletion.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to delete</param>
        /// <returns>Delete confirmation view with employee data, or NotFound if employee doesn't exist</returns>
        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Validate that an ID was provided
            if (id == null)
            {
                return NotFound();
            }

            // Find the employee to be deleted
            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);

            // Return NotFound if employee doesn't exist
            if (employee == null)
            {
                return NotFound();
            }

            // Show the delete confirmation page with employee details
            return View(employee);
        }

        /// <summary>
        /// Processes the actual deletion of an employee record.
        /// This action is called when the user confirms the deletion on the confirmation page.
        /// Includes comprehensive error handling and logging for audit purposes.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to delete</param>
        /// <returns>Redirects to Index with success/error message</returns>
        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")] // ActionName maps this to the Delete action name
        [ValidateAntiForgeryToken] // Protects against cross-site request forgery attacks
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Find the employee to delete
                var employee = await _context.Employees.FindAsync(id);
                
                if (employee != null)
                {
                    // Remove the employee from the database context
                    _context.Employees.Remove(employee);
                    // Save changes to actually delete from database
                    await _context.SaveChangesAsync();

                    // Log successful deletion for audit purposes
                    _logger.LogInformation("Employee {EmployeeName} deleted successfully", employee.FullName);
                    // Set success message for user feedback
                    TempData["Success"] = "Employee deleted successfully!";
                }
                else
                {
                    // Handle case where employee was already deleted or doesn't exist
                    TempData["Error"] = "Employee not found!";
                }
            }
            catch (Exception ex)
            {
                // Log any database or system errors
                _logger.LogError(ex, "Error deleting employee with ID {EmployeeId}", id);
                // Set error message for user feedback
                TempData["Error"] = "An error occurred while deleting the employee. Please try again.";
            }

            // Always redirect back to the employee list
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Helper method to check if an employee exists in the database.
        /// Used primarily for handling concurrency conflicts during updates.
        /// </summary>
        /// <param name="id">The employee ID to check for existence</param>
        /// <returns>True if employee exists, false otherwise</returns>
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
