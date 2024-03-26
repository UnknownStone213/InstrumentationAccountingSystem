using InstrumentationAccountingSystem.Common.Dto;
using InstrumentationAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentationAccountingSystem.BusinessLogic.Interfaces
{
    public interface IInstrumentationService
    {
        void Create(InstrumentationCreateDto InstrumentationCreateDto);

        List<Instrumentation> GetAll();

        void DeleteInstrumentationById(int id);

        void Edit(Instrumentation Instrumentation);

        Instrumentation GetInstrumentationById(int id);
    }
}
