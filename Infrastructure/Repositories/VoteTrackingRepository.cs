using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class VoteTrackingRepository : IVoteTrackingRepository
    {
        protected readonly PollingDbContext _dbContext;

        public VoteTrackingRepository(PollingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> HasVotedInLast24Hours(int pollId, string ip)
        {
            try
            {
                VoteTracking? lastVote = await _dbContext.Set<VoteTracking>()
                    .Where(v => v.PollId == pollId && v.UserIp == ip)
                    .OrderByDescending(v => v.VotedAt)
                    .FirstOrDefaultAsync();
                bool response = lastVote != null && (DateTime.UtcNow - lastVote.VotedAt).TotalHours < 24;
                return response;
            }
            catch
            {
                throw;
            }
        }

        public async Task RegisterVote(int pollId, string ip)
        {
            try
            {
                VoteTracking vote = new VoteTracking()
                {
                    PollId = pollId,
                    UserIp = ip,
                    VotedAt = DateTime.UtcNow
                };
                _dbContext.Set<VoteTracking>().Add(vote);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
