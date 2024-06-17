using System.ComponentModel.DataAnnotations;

namespace EmployeeApplication.Models.DTOs
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        [Required]
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public required string Gender { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required int Salary { get; set; }
        [Required]
        public required int DepartmentId { get; set; }
    }
}
