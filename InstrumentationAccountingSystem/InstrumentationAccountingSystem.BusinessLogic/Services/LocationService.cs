using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InstrumentationAccountingSystem.BusinessLogic.Interfaces;
using InstrumentationAccountingSystem.Common.Dto;
using InstrumentationAccountingSystem.Models;

namespace InstrumentationAccountingSystem.BusinessLogic.Services
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public LocationService(ApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }

        public void Create(LocationCreateDto locationCreateDto)
        {
            var location = _mapper.Map<LocationCreateDto, Location>(locationCreateDto);

            _applicationContext.Locations.Add(location);
            _applicationContext.SaveChanges();
        }
    }
}
