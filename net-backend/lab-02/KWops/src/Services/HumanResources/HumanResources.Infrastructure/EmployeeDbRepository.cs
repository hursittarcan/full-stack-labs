using HumanResources.AppLogic;
using HumanResources.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Infrastructure
{
    internal class EmployeeDbRepository : IEmployeeRepository
    {
        private readonly HumanResourcesContext _humanResourcesContext;

        public EmployeeDbRepository(HumanResourcesContext humanResourcesContext)
        {
            _humanResourcesContext = humanResourcesContext;
        }

        public async Task AddAsync(IEmployee newEmployee)
        {
            await _humanResourcesContext.AddAsync(newEmployee);
            await _humanResourcesContext.SaveChangesAsync();
        }

        public async Task<int> GetNumberOfStartersOnAsync(DateTime startDate)
        {
            return await _humanResourcesContext.Employees.CountAsync(x => x.StartDate == startDate);
        }

        async Task<IEmployee?> IEmployeeRepository.GetByNumberAsync(EmployeeNumber number)
        {
            return await _humanResourcesContext.Employees.FirstOrDefaultAsync(x => x.Number == number);
        }
    }
}
