using Application.DTOs.Poll;
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

namespace Application.Queries.Polls.GetById
{
    public class GetByIdPollQueryHandler : IRequestHandler<GetByIdPollQuery, PollReadDTO>
    {
        private readonly IPollRepository _pollRepository;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public GetByIdPollQueryHandler(IPollRepository pollRepository, IMapper mapper, IValidationService validationService)
        {
            _pollRepository = pollRepository;
            _mapper = mapper;
            _validationService = validationService;
        }

        public async Task<PollReadDTO> Handle(GetByIdPollQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _validationService.IsValidId(request.Id);
                Poll poll = await _pollRepository.GetById(request.Id);
                if(poll == null) throw new KeyNotFoundException($"Poll with ID {request.Id} not found");
                PollReadDTO pollDTO = _mapper.Map<PollReadDTO>(poll);
                return pollDTO;
            }
            catch (ArgumentException) { throw; }
            catch (KeyNotFoundException) { throw; }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while retrieving: {ex.Message}", ex);
            }
        }
    }
}
