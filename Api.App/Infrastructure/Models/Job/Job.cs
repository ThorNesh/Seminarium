using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.App.Infrastructure.Models.Worker;

namespace Api.App.Infrastructure.Models.Job
{
    public class Job
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Describe { get; set; }
        public string Status { get; set; }
        public DateTime DateOfIssue { get; set; } = new DateTime();
        public Worker.Worker Worker { get; set; } = new Worker.Worker();
        public Client Client { get; set; } = new Client();
        public Car.Car Car { get; set; } = new Car.Car();
    }
}
