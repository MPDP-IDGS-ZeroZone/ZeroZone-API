using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TiendaAPI.Data;
using TiendaAPI.Data.Request;

namespace TiendaAPI.Controllers
{
    public class AuthController : ControllerBase
{
    private readonly Auth _auth;

    public AuthController(Auth auth)
    {
        _auth = auth;
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] Login model)
    {
        var token = _auth.Authenticate(model.Mail, model.Pasword);

        if (token == null)
        {
            return Unauthorized(); // Credenciales no válidas
        }

        return Ok(new { Token = token });
    }
}
}