using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiTienda.Data;
using ApiTienda.Data.Models;
using ApiTienda.Data.Request;
using ApiTienda.Data.Response;
using Microsoft.IdentityModel.Tokens;

namespace TiendaAPI.Services
{
    public class AuthService
    {
        private readonly ProtibleDbContext _context;

        public AuthService (ProtibleDbContext context)
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

        public UsuariosSocioResponse FuncionMagica(string Token){
            int Id = 0;
            
            if(Token != ""){
                JwtSecurityTokenHandler receiveHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken token = (JwtSecurityToken)receiveHandler.ReadToken(Token);
            
                Id = Convert.ToInt32(token.Claims.First( c => c.Type == ClaimTypes.Actor).Value);
            }
            UsuariosSocioResponse UsuarioSocio = _context.UsuariosSocios.Where(UsuariosSocio => UsuariosSocio.Idsocio == Id).Select(p => new UsuariosSocioResponse
            {
                Idusuariosocio = p.Idusuariosocio,
                Pasword = p.Pasword,
                Rol = p.Rol,
                Mail = p.Mail,
                Idsocio = p.Idsocio
            }).First();

            return UsuarioSocio;
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

        public IEnumerable<AccountResponse> Get(int Id = 0, string Estatus = "", int Page = 1, int PageSize = 10)
        {
            IQueryable<UsuariosSocio> SocioQuery = _context.UsuariosSocios.AsQueryable();

            if (Id != 0)
            {
                SocioQuery = SocioQuery.Where(c => c.Idsocio == Id);
            }

            if (!string.IsNullOrEmpty(Estatus))
            {
                SocioQuery = SocioQuery.Where(c => c.Estatus == Estatus);
            }

            int skipAmount = (Page - 1) * PageSize;
            SocioQuery = SocioQuery.Skip(skipAmount).Take(PageSize);

            List<AccountResponse> response = SocioQuery.Select(p => new AccountResponse
            {
                Idsocio = p.Idsocio,
                Idusuariosocio = p.Idusuariosocio,
                Nombre = p.IdsocioNavigation.Nombre,
                Apellidos = p.IdsocioNavigation.Apellidos,
                FechaNacimiento = p.IdsocioNavigation.FechaNacimiento,
                Pasword = p.Pasword,
                Rol = p.Rol,
                Mail = p.Mail,
                Estatus = p.Estatus
            }).ToList();

            return response;
        }

        public AccountResponse GetById(int Id)
        {
            IQueryable<UsuariosSocio> SocioQuery = _context.UsuariosSocios.AsQueryable();
            SocioQuery = SocioQuery.Where(c => c.Idsocio == Id);

            List<AccountResponse> response = SocioQuery.Select(p => new AccountResponse
            {
                Idsocio = p.Idsocio,
                Idusuariosocio = p.Idusuariosocio,
                Nombre = p.IdsocioNavigation.Nombre,
                Apellidos = p.IdsocioNavigation.Apellidos,
                FechaNacimiento = p.IdsocioNavigation.FechaNacimiento,
                Pasword = p.Pasword,
                Rol = p.Rol,
                Mail = p.Mail,
                Estatus = p.Estatus
            }).ToList();

            return response.First();
        }

        public Socio GetByIdSocio(int Id)
        {
            IQueryable<Socio> SocioQuery = _context.Socios.AsQueryable();
            SocioQuery = SocioQuery.Where(c => c.Idsocio == Id);

            return SocioQuery.First();
        }

        public UsuariosSocio GetByIdUsuarioSocio(int Id)
        {
            IQueryable<UsuariosSocio> SocioQuery = _context.UsuariosSocios.AsQueryable();
            SocioQuery = SocioQuery.Where(c => c.Idsocio == Id);

            return SocioQuery.First();
        }

        public UsuariosSocio CreateUsuariosSocio(Account newAccount, int Idsocio)
        {
            UsuariosSocio UsuariosSocio = new UsuariosSocio();
            UsuariosSocio.Pasword = newAccount.Pasword;
            UsuariosSocio.Rol = newAccount.Rol;
            UsuariosSocio.Mail = newAccount.Mail;
            UsuariosSocio.Idsocio = Idsocio;

            _context.UsuariosSocios.Add(UsuariosSocio);
            _context.SaveChanges();

            return UsuariosSocio;
        }

        public Socio CreateSocio(Account newAccount)
        {
            Socio Socio = new Socio();
            Socio.Nombre = newAccount.Nombre;
            Socio.Apellidos = newAccount.Apellidos;
            Socio.FechaNacimiento = newAccount.FechaNacimiento;

            _context.Socios.Add(Socio);
            _context.SaveChanges();

            return Socio;
        }

        public AccountResponse Create(Account newAccount)
        {
            var Socio = CreateSocio(newAccount);
            var UsuariosSocio = CreateUsuariosSocio(newAccount,Socio.Idsocio);

            AccountResponse AccountResponse = new AccountResponse
            {
                Idsocio = Socio.Idsocio,
                Idusuariosocio = UsuariosSocio.Idusuariosocio,
                Nombre = Socio.Nombre,
                Apellidos = Socio.Apellidos,
                FechaNacimiento = Socio.FechaNacimiento,
                Pasword = UsuariosSocio.Pasword,
                Rol = UsuariosSocio.Rol,
                Mail = UsuariosSocio.Mail,
                Estatus = UsuariosSocio.Estatus
            };

            return AccountResponse;
        }

        public void UpdateSocio(int Id, Account Account)
        {
            var existingSocio = GetByIdSocio(Id);

            if (existingSocio is not null)
            {
                existingSocio.Nombre = Account.Nombre;
                existingSocio.Apellidos = Account.Apellidos;
                existingSocio.FechaNacimiento = Account.FechaNacimiento;

                _context.SaveChanges();
            }
        }

        public void UpdateUsuarioSocio(int Id, Account Account)
        {
            var existingSocio = GetByIdUsuarioSocio(Id);

            if (existingSocio is not null)
            {
                existingSocio.Pasword = Account.Pasword;
                existingSocio.Rol = Account.Rol;
                existingSocio.Mail = Account.Mail;

                _context.SaveChanges();
            }
        }

        public void Update(int Id, Account Account)
        {
            UpdateSocio(Id, Account);
            UpdateUsuarioSocio(Id, Account);
        }

        public void DeleteSocio(int Id)
        {
            var SocioToDelete = GetByIdSocio(Id);

            if (SocioToDelete is not null)
            {
                SocioToDelete.Estatus = "Eliminado"; // Actualiza el campo de estado
                _context.SaveChanges();
            }
        }

        public void DeleteUsuarioSocio(int Id)
        {
            var UsuarioSocioToDelete = GetByIdUsuarioSocio(Id);

            if (UsuarioSocioToDelete is not null)
            {
                UsuarioSocioToDelete.Estatus = "Eliminado"; // Actualiza el campo de estado
                _context.SaveChanges();
            }
        }

        public void Delete(int Id)
        {
            DeleteUsuarioSocio(Id);
            DeleteSocio(Id);
        }
    }
}