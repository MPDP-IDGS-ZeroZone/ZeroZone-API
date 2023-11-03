using System;
using System.Collections.Generic;

namespace ApiTienda.Data.Models;

public class DetallesVentaRequest
{
    public int Idproducto { get; set; }
    public int cantidad { get; set; }
}