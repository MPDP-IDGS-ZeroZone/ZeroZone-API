using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

        // Relación con UsuariosSocios
        public ICollection<Carrito> Carritos { get; set; }
        public int IdUsuarioSocio { get; set; }
        public UsuarioSocio UsuarioSocio { get; set; }
    }

}