using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TiendaApi.Data.Request;

public partial class CategoriaRequest
{
    public int Idcategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public string Foto { get; set; } = null!;
}
