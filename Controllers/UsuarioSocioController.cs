using Microsoft.AspNetCore.Mvc;
using ApiTienda.Data;
using ApiTienda.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace ApiTienda.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsuarioSocioController : Controller
    {
        private readonly TiendaBdContext _context;
        public UsuarioSocioController(TiendaBdContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IEnumerable<UsuariosSocio> Get()
        {
            return _context.UsuariosSocios.ToList();
        }
    }
}