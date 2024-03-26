using InstrumentationAccountingSystem.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentationAccountingSystem.BusinessLogic.Interfaces
{
    public interface ILocationService
    {
        void Create(LocationCreateDto locationCreateDto);
    }
}
