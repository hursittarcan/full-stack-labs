using DevOps.AppLogic;
using DevOps.Domain;
using DevOps.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Infrastructure
{
    internal class TeamRepository : ITeamRepository
    {
        private readonly DevOpsContext _devOpsContext;

        public TeamRepository(DevOpsContext devOpsContext)
        {
            _devOpsContext = devOpsContext;
        }

        public async Task<IReadOnlyList<Team>> GetAllAsync()
        {
            return await _devOpsContext.Teams.Include(team => team.Developers.ToList()).ToListAsync();
        }

        public async Task<Team?> GetByIdAsync(Guid teamId)
        {
            var team = await _devOpsContext.Teams.Include(team => team.Developers.ToList()).ToListAsync();
            return team.Where(team => team.Id == teamId).FirstOrDefault();
        }
    }
}
