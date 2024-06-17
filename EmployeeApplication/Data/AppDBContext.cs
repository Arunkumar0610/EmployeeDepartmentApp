using EmployeeApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
namespace EmployeeApplication.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);
            base.OnModelCreating(modelBuilder);
        }*/
    }
}
