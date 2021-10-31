using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.App.Infrastructure.Models.Job
{
    public class JobDB
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Describe { get; set; }
        public string Status { get; set; }
        public DateTime DateOfIssue { get; set; }
        public int WorkerId { get; set; }
        public int ClientId { get; set; }
        public int CarId { get; set; }
    }
}
