using Application.DTOs.Poll;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;


namespace Application.Queries.Polls.GetAll
{
    public class GetAllPollQueryHandler : IRequestHandler<GetAllPollsQuery, List<PollReadDTO>>
    {
        private readonly IPollRepository _pollRepository;
        private readonly IMapper _mapper;

        public GetAllPollQueryHandler(IPollRepository pollRepository, IMapper mapper)
        {
            _pollRepository = pollRepository;
            _mapper = mapper;
        }
        public async Task<List<PollReadDTO>> Handle(GetAllPollsQuery request, CancellationToken cancellationToken)
        {
            try

            {
                List<Poll> polls = await _pollRepository.GetAll(request.Page, request.PageSize);
                if (polls == null) return new List<PollReadDTO>();
                List<PollReadDTO> pollsDTO = _mapper.Map<List<PollReadDTO>>(polls);
                return pollsDTO;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while getting: {ex.Message}", ex);
            }
        }
    }
}
