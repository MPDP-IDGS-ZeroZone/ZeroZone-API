using System;
using System.Collections.Generic;

namespace ApiTienda.Data.Models;

public partial class VentaRequest
{
    public IEnumerable<DetallesVentaRequest> DetallesVenta { get; set; } = null!;
}