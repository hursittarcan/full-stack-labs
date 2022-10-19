using HumanResources.Api.Models;
using System;
using Test;

namespace HumanResources.Api.Tests.Builders
{
    internal class EmployeeCreateModelBuilder : BuilderBase<EmployeeCreateModel>
    {
        public EmployeeCreateModelBuilder()
        {
            Item = new EmployeeCreateModel
            {
                FirstName = Random.NextString(),
                LastName = Random.NextString(),
                StartDate = DateTime.Now
            };
        }
    }
}