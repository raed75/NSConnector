using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSConnector.Models
{
    public struct NSAuth
    {
        public int AccountID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
