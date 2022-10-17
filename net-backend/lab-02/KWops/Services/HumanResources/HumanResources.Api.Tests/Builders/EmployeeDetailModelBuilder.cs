using HumanResources.Api.Models;
using System;
using Test;

namespace HumanResources.Api.Tests.Builders
{
    internal class EmployeeDetailModelBuilder : BuilderBase<EmployeeDetailModel>
    {
        public EmployeeDetailModelBuilder()
        {
            Item = new EmployeeDetailModel
            {
                Number = Random.NextString(),
                FirstName = Random.NextString(),
                LastName = Random.NextString(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1)
            };
        }
    }
}
