using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTienda.Request
{
    public class ProductoRequest
    {
        public int Idproducto { get; set; }

        public string Nombre { get; set; } = null!;

        public decimal Precio { get; set; }

        public string Descripcion { get; set; } = null!;

        public string Categoria { get; set; } = null!;

        public int Stock { get; set; }

        public string Statusp { get; set; } = null!;

        public int Idusuariosocio { get; set; }
    }
}