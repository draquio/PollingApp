using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Votes.AddVote
{
    public class AddVoteCommand : IRequest<bool>
    {
        public AddVoteCommand(int pollId, int optionId, string userIp)
        {
            PollId = pollId;
            OptionId = optionId;
            UserIp = userIp;
        }

        public int PollId { get; set; }
        public int OptionId { get; set; }
        public string UserIp { get; set; }
    }
}
