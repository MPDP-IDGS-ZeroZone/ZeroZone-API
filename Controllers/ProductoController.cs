using Microsoft.AspNetCore.Mvc;
using ApiTienda.Services;
using ApiTienda.Data.Models;
using ApiTienda.Request;

namespace ApiTienda.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : Controller
    {
        private readonly ProductoService _service;
        public ProductoController(ProductoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<Producto>> Get(int Id = 0, int Categoria = 0, int SocioID = 0, string Nombre = "", bool DisponibleOnly = false)
        {
            var producto = _service.Get(Id, Categoria, SocioID, Nombre, DisponibleOnly);
            
            if (producto is null){
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpPost]
        public IActionResult Create(ProductoRequest producto)
        {
            var newProducto = _service.Create(producto);
            return CreatedAtAction(nameof(Get), new {Id = newProducto.Idproducto}, newProducto);
        }

        [HttpPut]
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