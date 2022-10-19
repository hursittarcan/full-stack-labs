using Domain;

namespace DevOps.Domain
{
    public class Developer : Entity
    {
        public string Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Percentage Rating { get; private set; }
        public Guid? TeamId { get; internal set; }

        private Developer(string id, string firstName, string lastName, Percentage rating)
        {
            Contracts.Require(!string.IsNullOrEmpty(id), "The identifier of a developer cannot be empty");
            Contracts.Require(!string.IsNullOrEmpty(firstName), "The last name of a developer cannot be empty");
            Contracts.Require(!string.IsNullOrEmpty(lastName), "The first name of a developer cannot be empty");
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Rating = rating;
        }

        public static Developer CreateNew(string id, string firstName, string lastName, Percentage rating)
        {
            return new Developer(id, firstName, lastName, rating);
        }

        protected override IEnumerable<object> GetIdComponents()
        {
            yield return Id;
        }
    }
}