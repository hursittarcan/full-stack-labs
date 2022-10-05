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

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet("{number}")]
        public async Task<IActionResult> GetEmployee(string number)
        {
            var result = await _employeeRepository.GetByNumberAsync(number);
            return new JsonResult(result); 
        }

        [HttpPost]
        public async void AddEmployee(Employee employee)
        {
            await _employeeRepository.AddAsync(employee);
        }
    }
}
