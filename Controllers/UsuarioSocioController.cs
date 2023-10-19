using Microsoft.AspNetCore.Mvc;
using TiendaApi.Data;
using TiendaApi.Data.Models;

namespace TiendaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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