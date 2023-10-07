using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
    public class Socios
    {
        public int IdSocio { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }

        // Relación con UsuariosSocios
        public ICollection<UsuarioSocio> UsuariosSocios { get; set; }
    }
}