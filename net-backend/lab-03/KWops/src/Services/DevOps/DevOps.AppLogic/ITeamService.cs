using DevOps.Domain;

namespace DevOps.AppLogic
{
    public interface ITeamService
    {
        Task AssembleDevelopersAsyncFor(Team team, int requiredNumberOfDevelopers);
    }
}
