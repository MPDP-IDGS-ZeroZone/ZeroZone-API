using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiTienda.Data.Request;

public partial class Account
{
    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string Pasword { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public string Mail { get; set; } = null!;
}
