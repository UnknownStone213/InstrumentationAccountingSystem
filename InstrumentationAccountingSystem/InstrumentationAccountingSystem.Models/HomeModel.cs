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
        public List<Instrumentation> Instrumentations { get; set; } = new List<Instrumentation>();
        public List<Type> Types { get; set; } = new List<Type>();
        public List<Location> Locations { get; set; } = new List<Location>();
        public List<Verification> Verifications { get; set; } = new List<Verification>();
    }
}
