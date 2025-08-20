using Employee_Directory.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Employee_Directory.Data
{
    /// <summary>
    /// Application database context that manages data access for the Employee Directory system.
    /// Extends IdentityDbContext to provide ASP.NET Core Identity functionality alongside
    /// custom business entities like Employee records.
    /// 
    /// This context handles:
    /// - User authentication and authorization data (via Identity)
    /// - Employee records and related operations
    /// - Database configuration and constraints
    /// - Initial data seeding for development and testing
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the ApplicationDbContext.
        /// </summary>
        /// <param name="options">Database context options including connection string and provider settings</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// DbSet representing the Employee table in the database.
        /// Provides access to CRUD operations for employee records.
        /// Used by Entity Framework for query generation and change tracking.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Configures the database schema, relationships, constraints, and seed data.
        /// This method is called by Entity Framework during database creation/migration
        /// to establish the database structure according to application requirements.
        /// </summary>
        /// <param name="builder">Model builder used to configure entity properties and relationships</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Call base method to configure Identity tables and relationships
            base.OnModelCreating(builder);

            // Configure Employee entity with explicit database constraints
            builder.Entity<Employee>(entity =>
            {
                // Primary key configuration
                entity.HasKey(e => e.EmployeeId);

                // Property constraints that match our model validation attributes
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Position).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Department).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
                entity.Property(e => e.HireDate).IsRequired();

                // Database indexes for improved query performance
                entity.HasIndex(e => e.FullName);      // For name-based searches
                entity.HasIndex(e => e.Department);    // For department filtering
                entity.HasIndex(e => e.Email).IsUnique(); // Ensures email uniqueness at DB level
            });

            // Seed initial employee data for development and demo purposes
            // This data is automatically inserted when the database is created
            builder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1,
                    FullName = "Dilina Mewan",
                    Email = "dilina@gmail.com",
                    Position = "Software Developer",
                    Department = "IT",
                    Phone = "0713336584",
                    HireDate = new DateTime(2022, 1, 15)
                },
                new Employee
                {
                    EmployeeId = 2,
                    FullName = "Thushara Dulshan",
                    Email = "thushara@gmail.com",
                    Position = "Project Manager",
                    Department = "IT",
                    Phone = "0715760142",
                    HireDate = new DateTime(2021, 6, 10)
                },
                new Employee
                {
                    EmployeeId = 3,
                    FullName = "Malinda Tanuj",
                    Email = "malinda@gmail.com",
                    Position = "HR Manager",
                    Department = "Human Resources",
                    Phone = "0719453677",
                    HireDate = new DateTime(2020, 3, 5)
                }
            );
        }
    }
}
