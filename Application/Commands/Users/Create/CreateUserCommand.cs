using Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users.Create
{
    public class CreateUserCommand : IRequest<UserReadDTO>
    {
        public UserCreateDTO User { get; set; }

        public CreateUserCommand(UserCreateDTO user)
        {
            User = user;
        }
    }
}
