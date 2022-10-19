using DevOps.AppLogic;
using DevOps.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Infrastructure
{
    internal class DeveloperRepository : IDeveloperRepository
    {
        private readonly DevOpsContext _devOpsContext;

        public DeveloperRepository(DevOpsContext devOpsContext)
        {
            _devOpsContext = devOpsContext;
        }

        public async Task AddAsync(Developer developer)
        {
            _devOpsContext.Developers.Add(developer);
            await _devOpsContext.SaveChangesAsync();
        }

        public async Task CommitTrackedChangesAsync()
        {
            await _devOpsContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Developer>> FindDevelopersWithoutATeamAsync()
        {
            return await _devOpsContext.Developers.Where(d => d.TeamId == null).ToListAsync();
        }

        public Task<Developer> GetByIdAsync(string number)
        {
            throw new NotImplementedException();
        }
    }
}
