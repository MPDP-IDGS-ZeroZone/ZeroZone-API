using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiTienda.Data.Models;

public partial class Oferta
{
    public int Idoferta { get; set; }

    public int Idproducto { get; set; }

    public int Porcentaje { get; set; }

    public DateTime Fechainicio { get; set; }

    public DateTime Fechacierre { get; set; }

    public string Estatus { get; set; } = null!;

    [JsonIgnore]
    public virtual Producto IdproductoNavigation { get; set; } = null!;
}
