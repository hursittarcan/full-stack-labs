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
        public IActionResult GetEmployee(string number)
        {
            var result = _employeeRepository.GetByNumberAsync(number);
            return new JsonResult(result); 
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            _employeeRepository.AddAsync(employee);
            return new JsonResult(true);
        }
    }
}
