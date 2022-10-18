using DevOps.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.AppLogic
{
    internal class TeamService : ITeamService
    {
        public TeamService(IDeveloperRepository developerRepository)
        {
        }

        public Task AssembleDevelopersAsyncFor(Team team, int requiredNumberOfDevelopers)
        {
            throw new NotImplementedException();
        }
    }
}
