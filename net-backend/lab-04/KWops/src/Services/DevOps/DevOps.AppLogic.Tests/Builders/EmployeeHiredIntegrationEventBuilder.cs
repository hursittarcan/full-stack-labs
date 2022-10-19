using DevOps.AppLogic.Events;
using System;
using Test;

namespace DevOps.AppLogic.Tests.Builders
{
    internal class EmployeeHiredIntegrationEventBuilder : BuilderBase<EmployeeHiredIntegrationEvent>
    {
        public EmployeeHiredIntegrationEventBuilder()
        {
            Item = new EmployeeHiredIntegrationEvent
            {
                Number = Random.NextString(),
                FirstName = Random.NextString(),
                LastName = Random.NextString()
            };
        }
    }
}