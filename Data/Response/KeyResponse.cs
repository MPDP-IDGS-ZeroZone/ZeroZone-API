using System;
using System.Collections.Generic;

namespace ApiTienda.Data.Response
{
    public class KeyResponse
    {
        public int Idkey { get; set; }

        public int Idproducto { get; set; }

        public string Keyproducto { get; set; } = null!;

        public string Estatus { get; set; } = null!;
    }
}
