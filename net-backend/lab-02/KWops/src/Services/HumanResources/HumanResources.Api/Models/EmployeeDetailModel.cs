namespace HumanResources.Api.Models
{
    public class EmployeeDetailModel
    {
        public string Number { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
