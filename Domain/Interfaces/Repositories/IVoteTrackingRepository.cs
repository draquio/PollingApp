using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IVoteTrackingRepository
    {
        Task<bool> HasVotedInLast24Hours(int pollId, string ip);
        Task RegisterVote(int pollId, string ip);
    }
}
