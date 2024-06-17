using System.ComponentModel.DataAnnotations;

namespace EmployeeApplication.Models.DTOs
{
    public class CreateDepartmentDTO
    {
        
        public int DepartmentId { get; set; }
        [Required]
        public required string DepartmentName { get; set; }
        [Required]
        public required string DepartmentCode { get; set; }
    }
}
