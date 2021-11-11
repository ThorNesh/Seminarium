using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarsztatAPI.Models
{
    public class Commision
    {
        public uint Id { get; set; }
        public Chains ChainId { get; set; } = new();
        public string Code { get; set; }
        public string DateOfStart { get; set; }
        public string HourOfStart { get; set; }
        public Status StatusId { get; set; } = new();
        public Worker WorkerId { get; set; } = new();
    }
}
