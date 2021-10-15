using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.App.Infrastructure.Models
{
    public class Engine
    {
        public uint Id_engine { get; set; }
        public uint Power { get; set; }
        public float EngineCapacity { get; set; }
        public string FuelType { get; set; }
    }
}
