using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IValidationService validationService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validationService = validationService;
        }
        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _validationService.IsValidId(request.User.Id);
                User user = await _userRepository.GetById(request.User.Id);
                if (user == null) throw new KeyNotFoundException($"User with ID {request.User.Id} not found");
                _mapper.Map(request.User, user);
                bool response = await _userRepository.Update(user);
                if(!response) throw new InvalidOperationException("User could not be updated");
                return response;

            }

            catch (ArgumentException) { throw; }
            catch (KeyNotFoundException) { throw; }
            catch (InvalidOperationException) { throw; }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while updating: {ex.Message}", ex);
            }
        }
    }
}
