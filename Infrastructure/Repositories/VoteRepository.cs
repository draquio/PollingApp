using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class VoteRepository : GenericRepository<Vote>, IVoteRepository
    {
        public VoteRepository(PollingDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> HasVotedInLast24Hours(int pollId, string? userIp, DateTime? timeToFilter = null)
        {
            try
            {
                if (userIp is null) return false;
                if (timeToFilter is null)
                {
                    timeToFilter = DateTime.UtcNow.AddHours(-24);
                }

                bool hasVoted = await _dbContext.Set<Vote>().AnyAsync(v => v.PollId == pollId && v.UserIp == userIp && v.VotedAt >= timeToFilter);
                return hasVoted;
            }
            catch 
            {
                throw;
            }
        }
    }
}
