using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarsztatAPI.Models
{
    public class Chains
    {
        public uint Id { get; set; }
        public Client ClientId { get; set; } = new();
        public Vehicle VehicleId { get; set; } = new();
        public string Message { get; set; }
        public string Service { get; set; }
    }
}
