using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.App.Infrastructure.Models.Car
{
    public class CarDB
    {
        public uint Id { get; set; }
        public string Vin { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public uint Vintage { get; set; }
        public uint Engine { get; set; }
    }
}
