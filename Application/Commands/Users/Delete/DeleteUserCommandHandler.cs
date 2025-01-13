using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidationService _validationService;

        public DeleteUserCommandHandler(IUserRepository userRepository, IValidationService validationService)
        {
            _userRepository = userRepository;
            _validationService = validationService;
        }
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _validationService.IsValidId(request.Id);
                User user = await _userRepository.GetById(request.Id);
                if (user == null) throw new KeyNotFoundException($"User with ID {request.Id} not found");
                bool response = await _userRepository.Delete(user);
                if (!response) throw new InvalidOperationException("User could not be deleted");
                return response;
            }
            catch (ArgumentException) { throw; }
            catch (KeyNotFoundException) { throw; }
            catch (InvalidOperationException) { throw; }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while deleting: {ex.Message}", ex);
            }
        }
    }
}
