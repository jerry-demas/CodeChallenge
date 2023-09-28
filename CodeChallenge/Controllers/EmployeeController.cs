using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;
        private readonly ICompensationService _compensationService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService, ICompensationService compensationService)
        {
            _logger = logger;
            _employeeService = employeeService;
            _compensationService = compensationService;
        }

        [HttpGet]
        public IActionResult GetEmployees() {
            _logger.LogDebug($"Received employee get request for all employees");

            var employees = _employeeService.GetAll();

            if (employees == null)
                return NotFound();

            return Ok(employees);

        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            _logger.LogDebug($"Received employee create request for '{employee.FirstName} {employee.LastName}'");

            _employeeService.Create(employee);

            return CreatedAtRoute("getEmployeeById", new { id = employee.EmployeeId }, employee);
        }

        [HttpGet("{id}", Name = "getEmployeeById")]
        public IActionResult GetEmployeeById(String id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var employee = _employeeService.GetById(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceEmployee(String id, [FromBody]Employee newEmployee)
        {
            _logger.LogDebug($"Recieved employee update request for '{id}'");

            var existingEmployee = _employeeService.GetById(id);
            if (existingEmployee == null)
                return NotFound();

            _employeeService.Replace(existingEmployee, newEmployee);

            return Ok(newEmployee);
        }
        

        //Reports
        [HttpGet("reports/{id}", Name = "getReportById")]
        public IActionResult GetReportById(String id)
        {
            _logger.LogDebug($"Received employee reports request for '{id}'");

            ReportingStructure structure = new ReportingStructure();
            Employee repEmployee = new Employee();

            Employee emp = _employeeService.GetById(id);

            if (emp == null)
            {
                return NotFound();

            } else
            {
                structure.Employee = emp;
                structure.NumberOfReports = emp.DirectReports == null ? 0 : emp.DirectReports.Count();

                if (emp.DirectReports != null && emp.DirectReports.Count > 0)
                {
                    //Get the direct reports for the 2nd level employees
                    foreach (var rep in emp.DirectReports)

                        repEmployee = _employeeService.GetById(rep.EmployeeId);

                    if (repEmployee != null && repEmployee.DirectReports != null && repEmployee.DirectReports.Count > 0)
                    {
                        structure.NumberOfReports += repEmployee.DirectReports.Count;
                    }
                }
            }
            
            return Ok(structure);
        }


        //Compensations

        [HttpGet("comp", Name = "getAllEmployeeCompensation")]
        public IActionResult GetEmployeeCompensation()
        {
            var comp = _compensationService.GetAll();
            if (comp == null)
                return NotFound();

            return Ok(comp);

        }

        [HttpGet("comp/{id}", Name = "getEmployeeCompensationById")]
        public IActionResult GetEmployeeCompensation(String id)
        {

            var comp = _compensationService.GetById(id);
            if (comp == null)
                return NotFound();

            return Ok(comp);

        }
        
        [HttpPost("AddComp")]
        public IActionResult CreateEmployeeCompensationById([FromBody] Compensation comp)
        {
            _logger.LogDebug($"Received employee compensation create request for '{comp.Employee.FirstName} {comp.Employee.LastName}'");
            
            _compensationService.Create(comp);

            return CreatedAtRoute("getEmployeeCompensationById", new { id = comp.Employee.EmployeeId }, comp);
            
        }

    }
}
