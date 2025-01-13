using Application.Services.Validation;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Polls.Delete
{
    public class DeletePollCommandHandler : IRequestHandler<DeletePollCommand, bool>
    {
        private readonly IPollRepository _pollRepository;
        private readonly IValidationService _validationService;

        public DeletePollCommandHandler(IPollRepository pollRepository, IValidationService validationService)
        {
            _pollRepository = pollRepository;
            _validationService = validationService;
        }

        public async Task<bool> Handle(DeletePollCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _validationService.IsValidId(request.Id);
                Poll poll = await _pollRepository.GetById(request.Id);
                if (poll == null) throw new KeyNotFoundException($"Poll with ID {request.Id} not found");
                bool response = await _pollRepository.Delete(poll);
                if (!response) throw new InvalidOperationException("Poll could not be deleted");
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
