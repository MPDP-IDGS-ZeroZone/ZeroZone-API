using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TiendaApi.Data.Request;

public partial class OfertaRequest
{
    public int Idoferta { get; set; }

    public int Idproducto { get; set; }

    public int Porcentaje { get; set; }

    public DateTime Fechainicio { get; set; }

    public DateTime Fechacierre { get; set; }

    public string Estatus { get; set; } = null!;
}
