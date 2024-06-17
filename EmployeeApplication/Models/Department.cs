using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeApplication.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId {  get; set; }
        [Required]
        public required string DepartmentName { get; set; }
        [Required]
        public required string DepartmentCode { get; set; }
        [JsonIgnore]
        public ICollection<Employee> Employees { get; set; }
    }
}
