using AutoMapper;
using SchoolOrganization.API.Models.SchoolModel;
using SchoolOrganization.API.Models.UserModel;
using SchoolOrganization.ServiceLayer.Contract.SchoolServices.Dtos;
using SchoolOrganization.ServiceLayer.Contract.UserServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOrganization.API.Models
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<SchoolModelDto, SchoolDto>().ReverseMap();
            CreateMap<AddUserModelDto, UserDto>().ReverseMap();
            CreateMap<EditUserModelDto, UserDto>().ReverseMap();
            


        }
    }
}
