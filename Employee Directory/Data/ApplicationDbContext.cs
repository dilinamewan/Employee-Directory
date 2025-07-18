using Employee_Directory.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Employee_Directory.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure Employee entity
            builder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Position).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Department).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
                entity.Property(e => e.HireDate).IsRequired();

                // Create index for better search performance
                entity.HasIndex(e => e.FullName);
                entity.HasIndex(e => e.Department);
                entity.HasIndex(e => e.Email).IsUnique();
            });

            
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
