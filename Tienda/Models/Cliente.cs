using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public string Rol { get; set; }

        // Relación con Carritos
        public ICollection<Carrito> Carritos { get; set; }
    }

}