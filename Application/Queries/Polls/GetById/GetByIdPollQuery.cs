using Application.DTOs.Poll;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Polls.GetById
{
    public class GetByIdPollQuery : IRequest<PollReadDTO>
    {
        public int Id { get; set; }

        public GetByIdPollQuery(int id)
        {
            Id = id;
        }
    }
}
