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
        IDeveloperRepository _developerRepository;
        Random _random; 

        public TeamService(IDeveloperRepository developerRepository)
        {
            _developerRepository = developerRepository;
            _random = new Random();
        }

        public async Task AssembleDevelopersAsyncFor(Team team, int requiredNumberOfDevelopers)
        {
            var developers = await _developerRepository.FindDevelopersWithoutATeamAsync();
            List<Developer> developersList = developers.Where(x => x.TeamId != team.Id).ToList();

            if (developers.Count >= requiredNumberOfDevelopers)
            {
                while (team.Developers.Count < requiredNumberOfDevelopers)
                {
                    Developer developer = developersList[_random.Next(0, developersList.Count)];
                    if (!team.Developers.Contains(developer)) team.Join(developer);
                }
            }
            else
            {
                foreach (Developer developer in developersList)
                {
                    team.Join(developer);
                }
            }
            await _developerRepository.CommitTrackedChangesAsync();
        }
    }
}
