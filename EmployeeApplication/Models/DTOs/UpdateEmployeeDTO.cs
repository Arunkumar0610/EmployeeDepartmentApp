using System.ComponentModel.DataAnnotations;

namespace EmployeeApplication.Models.DTOs
{
    public class UpdateEmployeeDTO
    {

        public  string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        public  string? Gender { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public int Salary { get; set; }
        public int DepartmentId { get; set; }
    }
}
