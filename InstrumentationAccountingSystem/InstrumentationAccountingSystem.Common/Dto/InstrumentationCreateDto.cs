using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentationAccountingSystem.Common.Dto
{
    public class InstrumentationCreateDto
    {
        public int TypeId { get; set; } // тип

        public string? Model { get; set; } // модель

        public string? FactoryNumber { get; set; } // заводской номер

        public int? LocationId { get; set; } // место установки

        public string? MeasurementLimits { get; set; } // пределы измерений

        //date // дата последней проверки

        public int Frequency { get; set; } // периодичность измерений

        public string? Connection { get; set; } // присоединение к процессу

        public string? CheckLocation { get; set; } // место поверки

        public string? Comment { get; set; } // примечание
    }
}
