using System;
using System.Collections.Generic;

namespace ApiTienda.Data.Models;

public partial class Producto
{
    public int Idproducto { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Categoria { get; set; } = null!;

    public int Stock { get; set; }

    public string Statusp { get; set; } = null!;

    public int Idusuariosocio { get; set; }

    public virtual UsuariosSocio IdusuariosocioNavigation { get; set; } = null!;
}
