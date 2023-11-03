using System;
using System.Collections.Generic;

namespace ApiTienda.Data.Models;

public partial class UsuariosSocio
{
    public int Idusuariosocio { get; set; }

    public string Pasword { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public int Idsocio { get; set; }

    public virtual Socio IdsocioNavigation { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
