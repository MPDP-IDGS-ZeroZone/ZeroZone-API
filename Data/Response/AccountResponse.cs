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
        public int Idsocio { get; set; }

        public int Idusuariosocio { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public DateTime FechaNacimiento { get; set; }
        
        public string Pasword { get; set; } = null!;

        public string Rol { get; set; } = null!;

        public string Mail { get; set; } = null!;

        public string Estatus { get; set; } = null!;
    }
}