using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentationAccountingSystem.Models
{
    public class HomeModel
    {
        public User? User { get; set; }

        public IEnumerable<Instrumentation> Instrumentations { get; set; } = new List<Instrumentation>();

        public IEnumerable<Type> Types { get; set; } = new List<Type>();

        public IEnumerable<Location> Locations { get; set; } = new List<Location>();
    }
}
