using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarsztatAPI.Models
{
    public class UserDB
    {
        public uint Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public uint Worker_Id { get; set; }
        public bool IsSuperUser { get; set; }
        public bool IsAdmin { get; set; }
    }
}
