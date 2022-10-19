using AppLogic.Events;

namespace HumanResources.AppLogic.Events
{
    public class EmployeeHiredIntegrationEvent : IntegrationEvent
    {
        public string Number { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
    }
}
