using AppLogic.Events;

namespace DevOps.AppLogic.Events
{
    public class EmployeeHiredIntegrationEvent : IntegrationEvent
    {
        public string Number { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
    }
}
