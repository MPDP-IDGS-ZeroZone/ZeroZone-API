using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiTienda.Data.Request
{
    public class KeyRequest
    {
        public int Idproducto { get; set; }

        public string Keyproducto { get; set; } = null!;

        public string Estatus { get; set; } = null!;
    }
}
