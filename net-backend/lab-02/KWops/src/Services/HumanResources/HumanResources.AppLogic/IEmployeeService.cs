using HumanResources.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.AppLogic
{
    public interface IEmployeeService
    {
        Task<IEmployee> HireNewAsync(string lastName, string firstName, DateTime startDate);
    }
}
