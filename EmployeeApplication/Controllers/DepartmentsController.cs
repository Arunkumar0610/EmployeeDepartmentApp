using AutoMapper;
using EmployeeApplication.Data;
using EmployeeApplication.Models;
using EmployeeApplication.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace EmployeeApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public DepartmentsController(AppDBContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetDepartments()
        {
            var departments =await _context.Departments.Include(e => e.Employees).ToListAsync();
            /* var departmentsdto = departments.Select(d => new DepartmentDTO
             {
                 DepartmentId = d.DepartmentId,
                 DepartmentName = d.DepartmentName,
                 DepartmentCode = d.DepartmentCode,
                 Employees=d.Employees.Select(e=>new EmployeeDTO { 
                     EmployeeId = e.EmployeeId,
                     FirstName = e.FirstName,
                     MiddleName = e.MiddleName,
                     LastName = e.LastName,
                     Gender = e.Gender,
                     Salary = e.Salary,
                     DepartmentId = e.DepartmentId
                 }).ToList()
             }).ToList();*/
            var departmentsdto = _mapper.Map<List<DepartmentDTO>>(departments);
            return Ok(departmentsdto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDTO>> GetDepartmentById(int id)
        {
            if(DepartmentExists(id))
            {
                var department=await _context.Departments.Include(q=>q.Employees).FirstOrDefaultAsync(e=>e.DepartmentId== id);
                var departmentdto=_mapper.Map<DepartmentDTO>(department);
                return Ok(departmentdto);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<DepartmentDTO>> CreateDepartment([FromBody] CreateDepartmentDTO department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.Departments.Any(x=>x.DepartmentName==department.DepartmentName || x.DepartmentCode==department.DepartmentCode))
            {
                return Conflict("Department already exists");
            }
            var dept= _mapper.Map<Department>(department);
            _context.Add(dept);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDepartmentById), new { id = dept.DepartmentId }, _mapper.Map<DepartmentDTO>(dept));
        }
      /*  [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDepartment(int id, DepartmentDTO department)
        {

        }*/
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDepartment(int id)
        {
            if(DepartmentExists(id))
            {
                var found= await _context.Departments.Include(x=>x.Employees).FirstOrDefaultAsync(x=>x.DepartmentId== id);
                foreach(var employee in found.Employees)
                {
                    _context.Employees.Remove(employee);
                }
                _context.Departments.Remove(found);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }
        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(x => x.DepartmentId == id);
        }
    }
}

