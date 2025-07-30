using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IVoteRepository : IGenericRepository<Vote>
    {
        Task<bool> HasVotedInLast24Hours(int pollId, string? userIp, DateTime? timeToFilter = null);
    }
}
