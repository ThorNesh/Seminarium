using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarsztatAPI.Models
{
    public class VehicleDB
    {
        public uint Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ProductionYear { get; set; }
        public string Vin { get; set; }
        public string RegistrationNumber { get; set; }
        public uint Engine_Power { get; set; }
        public float Engine_Capacity { get; set; }
        public uint FuelId { get; set; }
    }
}
