using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarsztatAPI.Models
{
    public class User
    {
        public uint Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Worker Worker_Id { get; set; } = new();
        public bool IsSuperUser { get; set; }
    }
}
