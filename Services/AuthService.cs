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

namespace TiendaAPI.Services
{
    public class AuthService
{
    private readonly TiendaBdContext _context;

    public AuthService (TiendaBdContext context)
    {
        _context = context;
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

        return new {Token = token};
    }

    public int FuncionMagica(string Token){
        int Id = 0;
        
        if(Token != ""){
            JwtSecurityTokenHandler receiveHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = (JwtSecurityToken)receiveHandler.ReadToken(Token);
        
            Id = Convert.ToInt32(token.Claims.First( c => c.Type == ClaimTypes.Actor).Value);
        }
        
        return Id;
    }

    private string GenerateJwtToken(UsuariosSocio user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("conexion@935algoritmosifrado9123@djdncosa"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Mail),
            new Claim(ClaimTypes.Role, user.Rol),
            new Claim(ClaimTypes.Actor, user.Idusuariosocio.ToString())
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