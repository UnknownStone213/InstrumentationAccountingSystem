using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace InstrumentationAccountingSystem.Models
{
    public class Verification
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateOnly Date { get; set; } // дата поверки
        public int InstrumentationId { get; set; } // СИ
        public Instrumentation Instrumentation { get; set; }
    }
}
