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

namespace Application.Queries.Users.GetById
{
    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, UserReadDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public GetByIdUserQueryHandler(IUserRepository userRepository, IMapper mapper, IValidationService validationService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validationService = validationService;
        }

        public async Task<UserReadDTO> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _validationService.IsValidId(request.Id);
                User user = await _userRepository.GetById(request.Id);
                if(user == null) throw new KeyNotFoundException($"User with ID {request.Id} not found");
                UserReadDTO userDTO = _mapper.Map<UserReadDTO>(user);
                return userDTO;
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
