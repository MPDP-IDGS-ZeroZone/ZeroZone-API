using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TiendaAPI.Data;
using TiendaAPI.Data.Models;

namespace TiendaAPI.Controllers
{
    [ApiController]
    [Route("Login")]
    public class AuthController : ControllerBase
{
    private readonly Auth _auth;

    public AuthController(Auth auth)
    {
        _auth = auth;
    }

    [HttpPost]
    public IActionResult Login([FromBody] Login model)
    {
        var token = _auth.Authenticate(model.Mail, model.Pasword);

        if (token == "")
        {
            return Unauthorized(); // Credenciales no válidas
        }

        return Ok(new { Token = token });
    }
}
}