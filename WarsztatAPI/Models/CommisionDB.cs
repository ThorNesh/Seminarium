using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarsztatAPI.Models
{
    public class CommisionDB
    {
        public uint Id { get; set; }
        public uint ChainId { get; set; }
        public string Code { get; set; }
        public string DateOfStart { get; set; }
        public string HourOfStart { get; set; }
        public uint StatusId { get; set; }
        public uint WorkerId { get; set; }
    }
}
