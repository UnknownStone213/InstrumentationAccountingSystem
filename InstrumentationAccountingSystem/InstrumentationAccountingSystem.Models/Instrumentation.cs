using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentationAccountingSystem.Models
{
    public class Instrumentation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TypeId { get; set; } // тип
        public Type Type { get; set; }
        [StringLength(50)]
        public string? Model { get; set; } // модель
        [StringLength(50)]
        public string? FactoryNumber { get; set; } // заводской номер
        public int? LocationId { get; set; } // место установки
        public Location? Location { get; set; }
        [StringLength(50)]
        public string? MeasurementLimits { get; set; } // пределы измерений
        [Required]
        public int Frequency { get; set; } // периодичность измерений
        [StringLength(50)]
        public string? Connection { get; set; } // присоединение к процессу
        [StringLength(250)]
        public string? Comment { get; set; } // примечание
    }
}
