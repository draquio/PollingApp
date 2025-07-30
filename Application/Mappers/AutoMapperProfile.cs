using Application.DTOs.Poll;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapeos

            #region User
            CreateMap<User, UserReadDTO>().ReverseMap();
            CreateMap<User, UserCreateDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();
            #endregion

            #region Poll
            CreateMap<Poll, PollReadDTO>()
                .ForMember(dto => dto.ExpirationDate, options => options.MapFrom(poll => poll.ExpirationDate.HasValue
                                                                                            ? poll.ExpirationDate.Value.ToString("dd/MM/yyyy HH:mm")
                                                                                            : null))
                .ForMember(dto => dto.CreatedAt, options => options.MapFrom(poll => poll.CreatedAt.ToString("dd/MM/yyyy")));

            CreateMap<PollOption, PollOptionDTO>()
                .ForMember(dto => dto.Option, options => options.MapFrom(pollOption => pollOption.OptionText))
                .ReverseMap();



            CreateMap<PollReadDTO, Poll>();
            CreateMap<PollCreateDTO, Poll>().ReverseMap();

            #endregion
        }
    }
}
