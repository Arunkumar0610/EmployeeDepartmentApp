using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeApplication.Models.DTOs
{
    public class DepartmentDTO
    {
        public int DepartmentId { get; set; }
        [Required]
        public required string DepartmentName { get; set; }
        [Required]
        public required string DepartmentCode { get; set; }
        public ICollection<EmployeeDTO> Employees { get; set; }
    }
}
