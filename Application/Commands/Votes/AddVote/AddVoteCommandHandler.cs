

using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Commands.Votes.AddVote
{
    public class AddVoteCommandHandler : IRequestHandler<AddVoteCommand, bool>
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IPollRepository _pollRepository;

        public AddVoteCommandHandler(IVoteRepository voteRepository, IPollRepository pollRepository)
        {
            _voteRepository = voteRepository;
            _pollRepository = pollRepository;
        }

        public async Task<bool> Handle(AddVoteCommand request, CancellationToken cancellationToken)
        {
            bool hasVoted = await _voteRepository.HasVotedInLast24Hours(request.Vote.PollId, request.UserIp);
            if (hasVoted) throw new InvalidOperationException("You have already voted in the last 24 hours.");

            Poll? poll = await _pollRepository.GetById(request.Vote.PollId);
            if (poll is null) throw new InvalidOperationException("Poll does not exist");
            if(!poll.Options.Any(o => o.Id == request.Vote.OptionId))
            {
                throw new InvalidOperationException("Selected option does not belong to this poll.");
            }
            var newVote = new Vote
            {
                PollId = request.Vote.PollId,
                UserIp = request.UserIp,
                OptionId = request.Vote.OptionId
            };
            await _voteRepository.Create(newVote);

            return true;
        }
    }
}

