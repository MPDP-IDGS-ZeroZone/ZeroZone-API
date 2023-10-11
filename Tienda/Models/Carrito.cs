using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
    public class Carrito
    {
        public int IdCarrito { get; set; }
        public DateTime FechaRegistro { get; set; }

        // Relaciones con Clientes y Productos
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }

        public int IdProducto { get; set; }
        public Producto Producto { get; set; }

        public decimal Monto { get; set; }
    }

}