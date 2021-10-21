using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.App.Infrastructure.Models
{
    public class WorkerLevel
    {
        public uint Id { get; set; }
        public string WorkerType { get; set; }
        public string WorkerStatus { get; set; }
        public string PermitType { get; set; }
    }
}
