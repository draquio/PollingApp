using Application.DTOs.Poll;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Polls.Create
{
    public record CreatePollCommand(PollCreateDTO Poll) : IRequest<PollReadDTO>;
}
