using System;
using System.Collections.Generic;

namespace ApiTienda.Data.Response
{
    public partial class PlataformaResponse
    {
        public int Idplataforma { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public string? UrlSitio { get; set; }
    }
}
