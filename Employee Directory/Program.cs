/**
 * Main entry point for the Employee Directory ASP.NET Core application.
 * 
 * This file configures all essential services including:
 * - Database connectivity with Entity Framework Core
 * - ASP.NET Core Identity for user authentication and authorization
 * - Cookie-based authentication and session management
 * - MVC controllers and views
 * - HTTP request pipeline and middleware
 * - Initial data seeding for development
 * 
 * The application follows the minimal hosting model introduced in .NET 6+
 * which provides a cleaner and more streamlined way to configure applications.
 */

using Employee_Directory.Data;
using Employee_Directory.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ================================================================
// SERVICE CONFIGURATION
// Configure all services needed by the application
// ================================================================

// Add support for MVC pattern with controllers and views
builder.Services.AddControllersWithViews();

// Configure Entity Framework with SQL Server database
// Connection string is read from appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure ASP.NET Core Identity for user authentication and authorization
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password requirements - configured for user-friendly development
    // In production, consider strengthening these requirements
    options.Password.RequireDigit = true;           // At least one number (0-9)
    options.Password.RequiredLength = 6;            // Minimum 6 characters
    options.Password.RequireNonAlphanumeric = false; // No special characters required
    options.Password.RequireUppercase = false;      // No uppercase letters required
    options.Password.RequireLowercase = false;      // No lowercase letters required

    // User account settings
    options.User.RequireUniqueEmail = true;         // Each email can only have one account
    options.SignIn.RequireConfirmedEmail = false;   // Allow login without email confirmation
})
.AddEntityFrameworkStores<ApplicationDbContext>()  // Use EF Core for Identity data storage
.AddDefaultTokenProviders();                       // Add token providers for password resets, etc.

// Configure authentication cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    // Define authentication-related paths
    options.LoginPath = "/Account/Login";           // Where to redirect for login
    options.LogoutPath = "/Account/Logout";         // Where to redirect for logout  
    options.AccessDeniedPath = "/Account/AccessDenied"; // Where to redirect when access is denied
    
    // Session and security settings
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Cookie expires after 30 minutes of inactivity
    options.SlidingExpiration = true;                   // Reset expiration time on each request
});

var app = builder.Build();

// ================================================================
// DATABASE INITIALIZATION AND DATA SEEDING
// Ensure database exists and has initial data for development/demo
// ================================================================

// Create a service scope to access database services during startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    // Ensure the database exists and is created with the current schema
    // In production, use proper database migrations instead
    context.Database.EnsureCreated();
    
    // Seed additional sample data if no employees exist beyond the seeded data
    // This provides extra demo data for development and testing purposes
    if (!context.Employees.Any())
    {
        var sampleEmployees = new List<Employee>
        {
            new Employee
            {
                FullName = "Ajith",
                Email = "ajith@company.com",
                Position = "Software Engineer",
                Department = "IT",
                Phone = "1234567890",
                HireDate = DateTime.Now.AddYears(-2) // Hired 2 years ago
            },
            new Employee
            {
                FullName = "Mewan",
                Email = "mewan@gmail.com",
                Position = "Project Manager",
                Department = "IT",
                Phone = "0987654321",
                HireDate = DateTime.Now.AddYears(-1) // Hired 1 year ago
            },
           
        };
        
        // Add the sample employees to the database
        context.Employees.AddRange(sampleEmployees);
        context.SaveChanges();
    }
}

// ================================================================
// HTTP REQUEST PIPELINE CONFIGURATION
// Configure middleware in the correct order for request processing
// ================================================================

// Configure different error handling based on environment
if (!app.Environment.IsDevelopment())
{
    // Production error handling - user-friendly error pages
    app.UseExceptionHandler("/Home/Error");  // Generic error page for unhandled exceptions
    app.UseHsts();                           // HTTP Strict Transport Security for HTTPS enforcement
}
else
{
    // Development error handling - detailed error information for debugging
    app.UseDeveloperExceptionPage();         // Shows detailed error information with stack traces
}

// Security and static file middleware
app.UseHttpsRedirection();                   // Redirect HTTP requests to HTTPS
app.UseStaticFiles();                        // Serve static files (CSS, JS, images) from wwwroot

// Routing middleware - enable routing for controllers and actions
app.UseRouting();

// Authentication and authorization middleware (order is important!)
app.UseAuthentication();                     // Identify who the user is (login status)
app.UseAuthorization();                      // Determine what the user can access (permissions)

// Configure the default MVC route pattern
// This enables conventional routing: /Controller/Action/Id
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");  // Default to Home/Index if no route specified

// Start the application and begin listening for HTTP requests
app.Run();
