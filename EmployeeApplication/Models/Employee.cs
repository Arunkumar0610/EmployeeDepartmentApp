using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApplication.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage ="Employee name is required")]
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public required string Gender { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required int Salary { get; set; }
        [Required]
        public required int  DepartmentId {get; set;}
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
    }
}
