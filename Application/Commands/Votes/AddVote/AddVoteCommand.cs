using Application.DTOs.Poll;
using Application.DTOs.Vote;
using MediatR;

namespace Application.Commands.Votes.AddVote
{
    public record AddVoteCommand(VoteCreateDTO Vote, string? UserIp) : IRequest<bool>;
}
