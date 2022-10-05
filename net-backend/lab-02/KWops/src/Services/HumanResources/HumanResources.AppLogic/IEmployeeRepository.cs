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
        Task AddAsync(Employee newEmployee);
        Task<Employee?> GetByNumberAsync(string number);
    }
}
