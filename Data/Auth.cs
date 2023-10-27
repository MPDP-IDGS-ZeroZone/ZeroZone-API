using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiTienda.Data;
using ApiTienda.Data.Models;
using Microsoft.IdentityModel.Tokens;

namespace TiendaAPI.Data
{
    public class Auth
{
    private readonly TiendaBdContext _context;
    private readonly IConfiguration _configuration;

    public Auth (TiendaBdContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public object Authenticate(string mail, string password)
    {
        var user = _context.UsuariosSocios.SingleOrDefault(u => u.Mail == mail && u.Pasword == password);

        if (user == null)
        {
            return ""; // Credenciales no válidas
        }

        // Generar token JWT
        var token = GenerateJwtToken(user);

        return new {
            Token = token,
            id = user.Idsocio
        };
    }

    private string GenerateJwtToken(UsuariosSocio user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("conexion@935algoritmosifrado9123@djdncosa"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Mail),
            new Claim(ClaimTypes.Role, user.Rol),
            // Puedes agregar más claims según tus necesidades
        };

        var token = new JwtSecurityToken(
            "http://localhost:5240/",
            "http://localhost:5240/",
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

}