using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Models
{
    public class ConnectionString : DbContext
    {
        public ConnectionString(DbContextOptions<ConnectionString> options) : base(options) 
        { 
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<PerformanceReview> PerformanceReviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = @"Server = TIPU\SQLEXPRESS;
                                  Database = EmployeeManagementSystemDB;
                                  Trusted_Connection = True;
                                  TrustServerCertificate = True;";

            optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasIndex(e => e.Name).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(e => e.Phone).IsUnique();

            modelBuilder.Entity<Department>().HasIndex(e => e.Name).IsUnique();

            modelBuilder.Entity<Department>()
            .HasOne(d => d.DepartmentManager) // Navigation property
            .WithMany() // No back-reference in Employee
            .HasForeignKey(d => d.ManagerID) // Foreign key property
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes
        }
    }
}
