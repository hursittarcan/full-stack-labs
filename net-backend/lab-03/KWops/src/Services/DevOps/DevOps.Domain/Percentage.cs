using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Domain
{
    public class Percentage : ValueObject<Percentage>
    {
        private readonly double _value;

        public Percentage(double value)
        {
            _value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value;
        }
    }
}
