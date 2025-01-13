using Application.DTOs.Poll;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Polls.GetAll
{
    public class GetAllPollsQuery : IRequest<List<PollReadDTO>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public GetAllPollsQuery(int page, int pageSize)
        {
            Page = page < 1 ? 1 : page;
            PageSize = pageSize < 1 ? 10 : pageSize;
        }
    }
}
