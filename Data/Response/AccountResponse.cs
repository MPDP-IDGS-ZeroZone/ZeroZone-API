using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTienda.Data;
using ApiTienda.Data.Models;

namespace ApiTienda.Data.Response
{
    public class AccountResponse
    {
        public Socio Socio { get; set; } = null!;

        public UsuariosSocio UsuariosSocio { get; set; } = null!;
    }
}