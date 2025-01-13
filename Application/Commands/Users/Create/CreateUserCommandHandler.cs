using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserReadDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserReadDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = _mapper.Map<User>(request.User);
                User userCreated = await _userRepository.Create(user);
                if(userCreated == null || userCreated.Id == 0) throw new InvalidOperationException("User could not be created");
                UserReadDTO userDTO = _mapper.Map<UserReadDTO>(userCreated);
                return userDTO;
            }
            catch (ArgumentException) { throw; }
            catch (InvalidOperationException) { throw; }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while creating: {ex.Message}", ex);
            }
        }
    }
}
