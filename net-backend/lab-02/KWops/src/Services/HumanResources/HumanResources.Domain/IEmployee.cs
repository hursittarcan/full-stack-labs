using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain
{
    public interface IEmployee
    {
        EmployeeNumber Number { get; }
        string LastName { get; }
        string FirstName { get; }
        DateTime StartDate { get; }
        DateTime? EndDate { get; }
    }
}
