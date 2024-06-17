using EmployeeApplication.Data;
using EmployeeApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using EmployeeApplication.Models.DTOs;
using AutoMapper;

namespace EmployeeApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;
        public EmployeesController(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _context.Employees
               .Include(e => e.Department)
               .ToListAsync();
            return employees;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.Include(x=>x.Department).FirstOrDefaultAsync(x=>x.EmployeeId==id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> CreateEmployee([FromBody] EmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           /* var employee = new Employee
            {
                FirstName = employeeDTO.FirstName,
                LastName = employeeDTO.LastName,
                Gender = employeeDTO.Gender,
                Email=employeeDTO.Email,
                Salary = employeeDTO.Salary,
                DepartmentId = employeeDTO.DepartmentId
            };*/
           if(!EmployeeExists(employeeDTO.Email))
            {
                var employee = _mapper.Map<Employee>(employeeDTO);

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.EmployeeId }, _mapper.Map<EmployeeDTO>(employee));
            }
            return Conflict("Employee with email already exists");
           
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (CheckEmployeeExists(id))
            {
                var employee= await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
                
               if (employee != null)
                {
                    employee.FirstName = employeeDTO.FirstName;
                    employee.LastName = employeeDTO.LastName;
                    employee.MiddleName = employeeDTO.MiddleName;
                    employee.Email = employeeDTO.Email;
                    employee.Salary = employeeDTO.Salary;
                    employee.Gender = employeeDTO.Gender;
                    employee.DepartmentId = employeeDTO.DepartmentId;
                    _context.Update(employee);
                }
               
                _context.SaveChangesAsync();
                return Ok("Updated employee");
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            if (CheckEmployeeExists(id))
            {
                var found = _context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
                _context.Remove(found);
                _context.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }
        private bool CheckEmployeeExists(int id)
        {
            var found = _context.Employees.Any(x => x.EmployeeId == id);
            return found;
        }
        private bool EmployeeExists(string email)
        {
            var found= _context.Employees.Any(x=>x.Email==email);
            return found;
        }

    }
}
