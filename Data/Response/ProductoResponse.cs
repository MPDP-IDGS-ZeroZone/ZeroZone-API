using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTienda.Data;
using ApiTienda.Data.Models;

namespace ApiTienda.Data.Response
{
    public class ProductoResponse
    {
        public int Idproducto { get; set; }

        public int Idusuariosocio { get; set; }

        public Socio Socio { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public decimal Precio { get; set; }

        public string Foto { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public Categoria Categoria { get; set; } = null!;

        public string Tipo { get; set; } = null!;

        public int Stock { get; set; }

        public string Statusp { get; set; } = null!;

        public IEnumerable<Oferta> Oferta { get; set; } = null!;
    }
}