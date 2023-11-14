using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiTienda.Data.Request
{
    public partial class PlataformaRequest
    {
        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public string? UrlSitio { get; set; }
    }
}
