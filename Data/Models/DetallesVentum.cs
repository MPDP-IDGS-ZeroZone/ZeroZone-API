using System;
using System.Collections.Generic;

namespace ApiTienda.Data.Models;

public partial class DetallesVentum
{
    public int Iddetalleventa { get; set; }

    public int Idventa { get; set; }

    public int Idproducto { get; set; }

    public int Cantidad { get; set; }

    public decimal Preciounitario { get; set; }

    public virtual Producto IdproductoNavigation { get; set; } = null!;

    public virtual Venta IdventaNavigation { get; set; } = null!;
}
