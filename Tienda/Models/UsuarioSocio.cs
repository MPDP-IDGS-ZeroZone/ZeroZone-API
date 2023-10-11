using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
    public class UsuarioSocio
    {
        public int IdUsuarioSocio { get; set; }
        public string Contraseña { get; set; }
        public string Rol { get; set; }
        public string Mail { get; set; }

        // Relación con Socios
        public int IdSocio { get; set; }
        public Socios Socio { get; set; }

        // Relación con Productos
        public ICollection<Producto> Productos { get; set; }
    }

}