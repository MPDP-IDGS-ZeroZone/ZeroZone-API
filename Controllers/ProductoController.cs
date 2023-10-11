using Microsoft.AspNetCore.Mvc;
using ApiTienda.Data;
using ApiTienda.Data.Models;

namespace ApiTienda.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : Controller
    {
        private readonly TiendaBdContext _context;
        public ProductoController(TiendaBdContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Producto> Get()
        {
            return _context.Productos.ToList();
        }
    }
}