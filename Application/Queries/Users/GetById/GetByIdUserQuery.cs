using Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Users.GetById
{
    public class GetByIdUserQuery : IRequest<UserReadDTO>
    {
        public int Id { get; set; }

        public GetByIdUserQuery(int id)
        {
            Id = id;
        }
    }
}
