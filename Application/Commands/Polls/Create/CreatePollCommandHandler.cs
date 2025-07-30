using Application.DTOs.Poll;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;
using Domain.Factories;
using Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Polls.Create
{
    public class CreatePollCommandHandler : IRequestHandler<CreatePollCommand, PollReadDTO>
    {
        private readonly PollFactory _factory;
        private readonly IPollRepository _pollRepository;
        private readonly IMapper _mapper;

        public CreatePollCommandHandler(PollFactory factory, IPollRepository pollRepository, IMapper mapper)
        {
            _factory = factory;
            _pollRepository = pollRepository;
            _mapper = mapper;
        }

        public async Task<PollReadDTO> Handle(CreatePollCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var pollAggregate = _factory.CreatePoll(request.Poll.Title, request.Poll.ExpirationDate);
                foreach (string option in request.Poll.Options)
                {
                    pollAggregate.AddOption(option);
                }
                Poll pollCreated = await _pollRepository.Create(pollAggregate.Poll);
                if (pollCreated == null || pollCreated.Id == 0) throw new InvalidOperationException("Poll could not be created");
                PollReadDTO pollDTO = _mapper.Map<PollReadDTO>(pollCreated);
                return pollDTO;
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
