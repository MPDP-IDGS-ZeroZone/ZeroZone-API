using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiTienda.Data.Models;

public partial class Socio
{
    public int Idsocio { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    [JsonIgnore]
    public virtual ICollection<UsuariosSocio> UsuariosSocios { get; set; } = new List<UsuariosSocio>();
}
