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
        public ActionResult<IEnumerable<Producto>> Get()
        {
            var producto = _service.Get();
            
            if (producto is null){
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpGet]
        [Route("ById")]
        public ActionResult<Producto> GetById(int Id)
        {
            var producto = _service.GetById(Id);
            
            if (producto is null){
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpGet]
        [Route("ByCategory")]
        public ActionResult<Producto> GetByCategory(string Categoria)
        {
            var producto = _service.GetByCategory(Categoria);
            
            if (producto is null){
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpGet]
        [Route("BySocio")]
        public ActionResult<Producto> GetBySocio(int SocioID)
        {
            var producto = _service.GetBySocio(SocioID);
            
            if (producto is null){
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpGet]
        [Route("SearchByNombre")]
        public ActionResult<Producto> SearchByNombre(string Nombre)
        {
            var producto = _service.SearchByNombre(Nombre);
            
            if (producto is null){
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpPost]
        public IActionResult Create(ProductoRequest producto)
        {
            var newProducto = _service.Create(producto);
            return CreatedAtAction(nameof(GetById), new {Id = newProducto.Idproducto}, newProducto);
        }

        [HttpPut]
        public IActionResult Update(int Id, ProductoRequest producto)
        {
            if(Id != producto.Idproducto)
            {
                return BadRequest();
            }
            var ProductoToUpdate = _service.GetById(Id).First();

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
            var ProductoToDelete = _service.GetById(Id).First();

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