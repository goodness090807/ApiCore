using APICore.Dtos.UserInfos;
using APICore.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICore.Profiles
{
    public class UserInfosProfile : Profile
    {
        public UserInfosProfile()
        {
            CreateMap<UserInfo, UserInfoReadDto>();
            CreateMap<UserInfoCreateDto, UserInfo>();
            CreateMap<UserInfoUpdateDto, UserInfo>();
        }
    }
}
