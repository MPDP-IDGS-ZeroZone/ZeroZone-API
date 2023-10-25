using Microsoft.AspNetCore.Mvc;
using ApiTienda.Services;
using ApiTienda.Data.Models;
using ApiTienda.Data.Request;
using Microsoft.AspNetCore.Authorization;
using System.Web.Http.Cors;

namespace ApiTienda.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductoController : Controller
    {
        private readonly ProductoService _service;
        public ProductoController(ProductoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Producto>> Get(int Id = 0, int Categoria = 0, int SocioID = 0, string Nombre = "", bool DisponibleOnly = false)
        {
            var producto = _service.Get(Id, Categoria, SocioID, Nombre, DisponibleOnly);
            
            if (producto is null){
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(ProductoRequest producto)
        {
            var newProducto = _service.Create(producto);
            return CreatedAtAction(nameof(Get), new {Id = newProducto.Idproducto}, newProducto);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update(int Id, ProductoRequest producto)
        {
            if(Id != producto.Idproducto)
            {
                return BadRequest();
            }
            var ProductoToUpdate = _service.Get(Id).First();

            if(ProductoToUpdate is not null)
            {
                _service.Update(Id, producto);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int Id)
        {
            var ProductoToDelete = _service.Get(Id).First();

            if(ProductoToDelete is not null)
            {
                _service.Delete(Id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}