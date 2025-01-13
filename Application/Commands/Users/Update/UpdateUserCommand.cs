using Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users.Update
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public UserUpdateDTO User { get; set; }

        public UpdateUserCommand(UserUpdateDTO user)
        {
            User = user;
        }
    }
}
