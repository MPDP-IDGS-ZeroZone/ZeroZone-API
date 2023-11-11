using System;
using System.Collections.Generic;

namespace ApiTienda.Data.Response;

public partial class CategoriaResponse
{
    public int Idcategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Foto { get; set; }
}
