using Microsoft.AspNetCore.Mvc;
using TiendaApi.Data;
using TiendaApi.Data.Models;

namespace TiendaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SocioController : Controller
    {
        private readonly TiendaBdContext _context;
        public SocioController(TiendaBdContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Socio> Get()
        {
            return _context.Socios.ToList();
        }
    }
}