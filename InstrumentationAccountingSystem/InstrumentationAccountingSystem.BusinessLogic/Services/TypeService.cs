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
    public class TypeService : ITypeService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public TypeService(ApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }

        public void Create(TypeCreateDto typeCreateDto)
        {
            var type = _mapper.Map<TypeCreateDto, InstrumentationAccountingSystem.Models.Type>(typeCreateDto);

            _applicationContext.Types.Add(type);
            _applicationContext.SaveChanges();
        }

        public List<InstrumentationAccountingSystem.Models.Type> GetAll()
        {
            var types = _applicationContext.Types.ToList();

            return types;
        }

        public void EditType(InstrumentationAccountingSystem.Models.Type type)
        {
            _applicationContext.Types.Update(type);
            _applicationContext.SaveChanges();
        }

        public void DeleteTypeById(int id)
        {
            var type = _applicationContext.Types.FirstOrDefault(u => u.Id == id);

            _applicationContext.Types.Remove(type);
            _applicationContext.SaveChanges();
        }

        public InstrumentationAccountingSystem.Models.Type GetTypeById(int id)
        {
            var type = _applicationContext.Types.FirstOrDefault(u => u.Id == id);

            return type;
        }
    }
}
