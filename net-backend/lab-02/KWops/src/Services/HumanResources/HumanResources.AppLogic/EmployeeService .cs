using HumanResources.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.AppLogic
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeFactory _employeeFactory;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeFactory employeeFactory, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeFactory = employeeFactory; 
        }

        public async Task<IEmployee> HireNewAsync(string lastName, string firstName, DateTime startDate)
        {
            //int sequence1 = await _employeeRepository.GetNumberOfStartersOnAsync(startDate);
            //var newEmployee1 = _employeeFactory.CreateNew(lastName, firstName, startDate, sequence1); 
            // _employeeRepository.AddAsync(newEmployee1);
            //return null;

            int sequence = await _employeeRepository.GetNumberOfStartersOnAsync(startDate) + 1;
            IEmployee newEmployee = _employeeFactory.CreateNew(lastName, firstName, startDate, sequence);
            await _employeeRepository.AddAsync(newEmployee);
            return newEmployee;
        }
    }
}
