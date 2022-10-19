using System;
using System.Collections.Generic;
using System.Reflection;
using Test;

namespace DevOps.Domain.Tests.Builders
{
    public class TeamBuilder : BuilderBase<Team>
    {
        private readonly List<Developer> _developers;

        public TeamBuilder()
        {
            Item = Team.CreateNew(Random.NextString());

            FieldInfo? developersBackingField = typeof(Team).GetField("_developers", BindingFlags.NonPublic | BindingFlags.Instance);
            _developers = developersBackingField?.GetValue(Item) as List<Developer> ??
                          throw new Exception("Cannot find _developers backing field of Team");
        }

        public TeamBuilder WithDeveloper(Developer developer)
        {
            _developers.Add(developer);
            return this;
        }
    }
}
