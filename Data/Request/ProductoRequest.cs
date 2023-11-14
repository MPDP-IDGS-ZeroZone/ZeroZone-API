using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTienda.Data.Request
{
    public class ProductoRequest
    {
        public int Idplataforma {get; set;}
        
        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public decimal Precio { get; set; }

        public string Foto { get; set; } = null!;

        public int IdCategoria { get; set; }

        public string Tipo { get; set; } = null!;

        public int Stock { get; set; }

        public string Statusp { get; set; } = null!;

        public IEnumerable<string> Keys { get; set;} = new List<string>();
    }
}