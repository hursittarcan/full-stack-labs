namespace HumanResources.Api.Models
{
    public class EmployeeCreateModel
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
    }
}