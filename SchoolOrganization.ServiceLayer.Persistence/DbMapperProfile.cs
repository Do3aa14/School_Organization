using AutoMapper;
using SchoolOrganization.Domain.Entities;
using SchoolOrganization.ServiceLayer.Contract.SchoolServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOrganization.ServiceLayer.Persistence
{
   public  class DbMapperProfile : Profile
    {
        public DbMapperProfile()
        {
            CreateMap<School, SchoolDto>().ReverseMap();
        }
    }
}
