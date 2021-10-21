using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.App.Infrastructure.Models.Worker
{
    public class WorkerDB
    {
        public uint WorkerId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public uint WorkerLevel { get; set; }
    }
}
