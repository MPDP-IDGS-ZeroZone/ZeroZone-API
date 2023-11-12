using System;
using System.Collections.Generic;

namespace ApiTienda.Data.Models;

public partial class Categoria
{
    public int Idcategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public string Foto { get; set; } = null!;

    public string Estatus { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
