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

        public async Task AddAsync(Employee newEmployee)
        {
            await _humanResourcesContext.AddAsync(newEmployee);
            _humanResourcesContext.SaveChangesAsync();
        }

        public async Task<Employee?> GetByNumberAsync(string number)
        {
            return await _humanResourcesContext.Set<Employee>().FirstOrDefaultAsync(x => x.Number == number);
        }
    }
}
