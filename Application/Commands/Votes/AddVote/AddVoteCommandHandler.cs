using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using MediatR;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Votes.AddVote
{
    public class AddVoteCommandHandler : IRequestHandler<AddVoteCommand, bool>
    {
        private readonly IPollRepository _pollRepository;
        private readonly IVoteTrackingRepository _voteTrackingRepository;
        private readonly INotificationService _notificationService;

        public AddVoteCommandHandler(IPollRepository pollRepository, IVoteTrackingRepository voteTrackingRepository, INotificationService notificationService)
        {
            _pollRepository = pollRepository;
            _voteTrackingRepository = voteTrackingRepository;
            _notificationService = notificationService;
        }

        public async Task<bool> Handle(AddVoteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool hasVoted = await _voteTrackingRepository.HasVotedInLast24Hours(request.PollId, request.UserIp);
                if (hasVoted) throw new InvalidOperationException("You have already voted in the last 24 hours.");

                Poll poll = await _pollRepository.GetById(request.PollId);
                var options = poll.Options.FirstOrDefault(o => o.Id == request.OptionId);
                if (options == null) throw new KeyNotFoundException("Option not found.");
                options.AddVote();

                await _pollRepository.Update(poll);
                await _voteTrackingRepository.RegisterVote(request.PollId, request.UserIp);
                await _notificationService.NotifyVoteReceived(request.PollId, request.OptionId, options.Votes);

                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
