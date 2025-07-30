//using Domain.Interfaces.Services;
//using Infrastructure.SignalR;
//using Microsoft.AspNetCore.SignalR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Infrastructure.Services
//{
//    public class SignalRNotificationService : INotificationService
//    {
//        private readonly IHubContext<VotingHub> _hubContext;

//        public SignalRNotificationService(IHubContext<VotingHub> hubContext)
//        {
//            _hubContext = hubContext;
//        }

//        public async Task NotifyVoteReceived(int pollId, int optionId, int votes)
//        {
//            await _hubContext.Clients.All.SendAsync("ReceiveVote", pollId, optionId, votes);
//        }
//    }
//}
