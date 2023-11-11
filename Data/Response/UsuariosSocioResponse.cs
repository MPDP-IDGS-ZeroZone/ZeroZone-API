using System;
using System.Collections.Generic;

namespace ApiTienda.Data.Response;

public partial class UsuariosSocioResponse
{
    public int Idusuariosocio { get; set; }

    public string Pasword { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public int Idsocio { get; set; }
}
