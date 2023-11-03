using System;
using System.Collections.Generic;

namespace ApiTienda.Data.Models;

public partial class Plataforma
{
    public int Idplataforma { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? UrlSitio { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
