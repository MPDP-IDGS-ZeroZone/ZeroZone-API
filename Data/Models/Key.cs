using System;
using System.Collections.Generic;

namespace ApiTienda.Data.Models;

public partial class Key
{
    public int Idkey { get; set; }

    public int Idproducto { get; set; }

    public string Keyproducto { get; set; } = null!;

    public string Estatus { get; set; } = null!;

    public virtual Producto IdproductoNavigation { get; set; } = null!;
}
