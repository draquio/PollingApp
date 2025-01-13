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

namespace Application.Queries.Users.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserReadDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserReadDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<User> users = await _userRepository.GetAll(request.Page, request.PageSize);
                if (users == null || !users.Any()) return new List<UserReadDTO>();
                List<UserReadDTO> listUsersDTO = _mapper.Map<List<UserReadDTO>>(users);
                return listUsersDTO;

            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while retrieving: {ex.Message}", ex);
            }
        }
    }
}
