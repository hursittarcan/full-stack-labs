using HumanResources.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.AppLogic
{
    public interface IEmployeeRepository
    {
        Task AddAsync(IEmployee newEmployee);
        Task<IEmployee?> GetByNumberAsync(EmployeeNumber number);
        Task<int> GetNumberOfStartersOnAsync(DateTime startDate);
    }
}
