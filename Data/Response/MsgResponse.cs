using System;
using System.Collections.Generic;

namespace ApiTienda.Data.Response;

public partial class MsgResponse
{
    public int Id { get; set; }

    public string Msg { get; set; } = null!;
}
