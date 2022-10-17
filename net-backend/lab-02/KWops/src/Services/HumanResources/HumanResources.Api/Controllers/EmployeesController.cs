using HumanResources.Api.Models;
using HumanResources.AppLogic;
using HumanResources.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.Api.Controllers
{
    [Route("/employees")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeRepository employeeRepository, IEmployeeService employeeService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }

        [HttpGet("{number}")]
        public async Task<IActionResult> GetByNumber(string number)
        {
            IEmployee? employee = await _employeeRepository.GetByNumberAsync(number);
            return employee == null ? NotFound() : Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EmployeeCreateModel model)
        {
            IEmployee hiredEmployee = await _employeeService.HireNewAsync(model.LastName, model.FirstName, model.StartDate);
            return CreatedAtAction(nameof(GetByNumber), new { number = hiredEmployee.Number }, hiredEmployee);
        }
    }
}
