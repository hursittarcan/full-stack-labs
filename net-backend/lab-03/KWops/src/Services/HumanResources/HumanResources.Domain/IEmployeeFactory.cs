using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain
{
    public interface IEmployeeFactory
    {
        IEmployee CreateNew(string lastName, string firstName, DateTime startDate, int sequence);
    }
}
