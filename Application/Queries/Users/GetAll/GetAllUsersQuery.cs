using Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Users.GetAll
{
    public class GetAllUsersQuery : IRequest<List<UserReadDTO>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public GetAllUsersQuery(int page, int pageSize)
        {
            Page = page < 1 ? 1 : page;
            PageSize = pageSize < 1 ? 10 : pageSize;
        }
    }
}
