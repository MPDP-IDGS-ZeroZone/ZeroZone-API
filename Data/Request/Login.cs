using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaAPI.Data.Request
{
    public class Login
    {
        public string Mail { get; set; } = null!;
        public string Pasword { get; set; } = null!;
    }
}