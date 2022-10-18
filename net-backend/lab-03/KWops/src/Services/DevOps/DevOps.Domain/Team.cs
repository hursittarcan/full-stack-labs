using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Domain
{
    public class Team : Entity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        private readonly List<Developer> _developers;
        public IReadOnlyList<Developer> Developers => _developers;

        private Team(Guid id, string name)
        {
            _developers = new List<Developer>();
        }

        public static Team CreateNew(string name)
        {
            return null;
        }

        public void Join(Developer developer)
        {

        }

        protected override IEnumerable<object> GetIdComponents()
        {
            yield return Id;
        }
    }
}
