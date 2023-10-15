using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiTienda.Data.Models;

public partial class UsuariosSocio
{
    public int Idusuariosocio { get; set; }

    public string Pasword { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public int Idsocio { get; set; }

    [JsonIgnore]
    public virtual Socio IdsocioNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
