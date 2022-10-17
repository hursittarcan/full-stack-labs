using AutoMapper;
using HumanResources.Api.Models;
using HumanResources.AppLogic;
using HumanResources.Domain;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeesController(IEmployeeService employeeService,
        IEmployeeRepository employeeRepository,
        IMapper mapper)
    {
        _employeeService = employeeService;
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    [HttpGet("{number}")]
    public async Task<IActionResult> GetByNumber(string number)
    {
        IEmployee? employee = await _employeeRepository.GetByNumberAsync(number);
        return employee == null ? NotFound() : Ok(_mapper.Map<EmployeeDetailModel>(employee));
    }

    [HttpPost]
    public async Task<IActionResult> Add(EmployeeCreateModel model)
    {
        IEmployee hiredEmployee = await _employeeService.HireNewAsync(model.LastName, model.FirstName, model.StartDate);
        var outputModel = _mapper.Map<EmployeeDetailModel>(hiredEmployee);
        return CreatedAtAction(nameof(GetByNumber), new { number = outputModel.Number }, outputModel);
    }
}
