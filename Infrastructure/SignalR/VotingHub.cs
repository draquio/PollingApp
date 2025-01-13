using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SignalR
{
    public class VotingHub : Hub
    {
        public async Task Vote(int pollId, int optionId)
        {
            await Clients.All.SendAsync("ReceiveVote", pollId, optionId);
        }
    }
}
