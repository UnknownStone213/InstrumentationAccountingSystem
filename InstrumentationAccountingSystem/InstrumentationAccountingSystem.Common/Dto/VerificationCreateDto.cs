using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentationAccountingSystem.Common.Dto
{
    public class VerificationCreateDto
    {
        public DateOnly Date { get; set; } // дата поверки

        public int InstrumentationId { get; set; } // КИП
    }
}
