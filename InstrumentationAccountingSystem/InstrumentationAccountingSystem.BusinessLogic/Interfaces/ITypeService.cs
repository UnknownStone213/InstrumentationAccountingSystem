using InstrumentationAccountingSystem.Common.Dto;
using InstrumentationAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentationAccountingSystem.BusinessLogic.Interfaces
{
    public interface ITypeService
    {
        void Create(TypeCreateDto typeCreateDto);

        List<InstrumentationAccountingSystem.Models.Type> GetAll();

        void EditType(InstrumentationAccountingSystem.Models.Type type);

        void DeleteTypeById(int id);

        InstrumentationAccountingSystem.Models.Type GetTypeById(int id);
    }
}
