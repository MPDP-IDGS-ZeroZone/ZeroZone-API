using System;
using System.Collections.Generic;

namespace ApiTienda.Data.Models;

public partial class Venta
{
    public int Idventa { get; set; }

    public int Idusuariosocio { get; set; }

    public DateTime Fechaventa { get; set; }

    public string Estatus { get; set; } = null!;

    public virtual ICollection<DetallesVentum> DetallesVenta { get; set; } = new List<DetallesVentum>();

    public virtual UsuariosSocio IdusuariosocioNavigation { get; set; } = null!;
}
