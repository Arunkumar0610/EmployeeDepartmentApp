using AutoMapper;
using EmployeeApplication.Models.DTOs;
using EmployeeApplication.Models;

namespace EmployeeApplication
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateDepartmentDTO, Department>(); // CreateMap for Department creation
            CreateMap<EmployeeDTO, Employee>(); // CreateMap for Employee creation
            CreateMap<Department, DepartmentDTO>(); // CreateMap for Department read (DTO)
            CreateMap<Employee, EmployeeDTO>(); // CreateMap for Employee read (DTO)
        }
    }
}