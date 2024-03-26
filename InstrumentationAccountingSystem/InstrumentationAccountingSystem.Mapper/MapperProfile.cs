using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InstrumentationAccountingSystem.Common.Dto;
using InstrumentationAccountingSystem.Models;

namespace InstrumentationAccountingSystem.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<TypeCreateDto, InstrumentationAccountingSystem.Models.Type>();
            CreateMap<LocationCreateDto, Location>();
            CreateMap<InstrumentationCreateDto, Instrumentation>();
            //CreateMap<UserLogInDto, User>();
            //CreateMap<NoteCreateDto, Note>();
        }
    }
}
