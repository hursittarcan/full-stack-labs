using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain
{
    internal class Employee : Entity, IEmployee
    {
        [Key]
        public EmployeeNumber Number { get; private set; }
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        private Employee()
        {
            Number = new EmployeeNumber(DateTime.MinValue, 1);
            LastName = string.Empty;
            FirstName = string.Empty;
        }

        protected override IEnumerable<object> GetIdComponents()
        {
            throw new NotImplementedException();
        }

        internal class Factory : IEmployeeFactory
        {
            public IEmployee CreateNew(string lastName, string firstName, DateTime startDate, int sequence)
            {
                Contracts.Require(startDate >= DateTime.Now.AddYears(-1), "The start date of an employee cannot be more than 1 year in the past");
                Contracts.Require(!string.IsNullOrEmpty(lastName), "The last name of an employee cannot be empty");
                Contracts.Require(lastName.Length >= 2, "The last name of an employee must at least have 2 characters");
                Contracts.Require(!string.IsNullOrEmpty(firstName), "The first name of an employee cannot be empty");
                Contracts.Require(firstName.Length >= 2, "The first name of an employee must at least have 2 characters");

                var employee = new Employee
                {
                    Number = new EmployeeNumber(startDate, sequence),
                    FirstName = firstName,
                    LastName = lastName,
                    StartDate = startDate,
                    EndDate = null
                };
                return employee;
            }
        }
    }
}
