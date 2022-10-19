using DevOps.Domain;

namespace DevOps.AppLogic
{
    public interface IDeveloperRepository
    {
        Task<IReadOnlyList<Developer>> FindDevelopersWithoutATeamAsync();
        Task CommitTrackedChangesAsync();
    }
}
