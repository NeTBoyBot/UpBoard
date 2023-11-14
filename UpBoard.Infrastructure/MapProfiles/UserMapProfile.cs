using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Board.Contracts.User;
using UpBoard.Contracts.User;
using UpBoard.Domain;

namespace Doska.AppServices.MapProfile
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<User, InfoUserResponse>().ForMember(u=>u.IsVerified,dest=>dest.MapFrom(src=>src.UserState == UpBoard.Domain.UserStates.UserStates.Verified)).ReverseMap();
            CreateMap<User, LoginUserRequest>().ReverseMap();
            CreateMap<User, RegisterUserRequest>().ReverseMap();
            CreateMap<User, RegisterUserResponse>().ReverseMap();
            CreateMap<User, EditUserRequest>().ReverseMap();
            CreateMap<User, DeleteUserRequest>().ReverseMap();
            CreateMap<InfoUserResponse, EditUserRequest>().ReverseMap();
        }
    }
}
