using Application.DTOs.Poll;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Polls.Create
{
    public class CreatePollCommand : IRequest<PollReadDTO>
    {
        public PollCreateDTO Poll;

        public CreatePollCommand(PollCreateDTO poll)
        {
            Poll = poll;
        }
    }
}
