
using Domain.Entities;

namespace Application.DTOs.Vote
{
    public class VoteCreateDTO
    {
        public int PollId { get; set; }
        public int OptionId { get; set; }
    }
}
