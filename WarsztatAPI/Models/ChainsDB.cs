using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarsztatAPI.Models
{
    public class ChainsDB
    {
        public uint Id { get; set; }
        public uint ClientId { get; set; }
        public uint VehicleId { get; set; }
        public string Message { get; set; }
        public string Service { get; set; }

    }
}
