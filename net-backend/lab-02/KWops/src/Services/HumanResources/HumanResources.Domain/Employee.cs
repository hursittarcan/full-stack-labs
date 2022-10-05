using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain
{
    public class Employee
    {
        [Key]
        public string? Number { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
