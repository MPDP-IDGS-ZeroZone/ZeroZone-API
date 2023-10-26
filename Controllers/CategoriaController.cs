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
    [DisableCors]
    public class CategoriaController : Controller
    {
        private readonly CategoriaService _service;
        public CategoriaController(CategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get(int Id = 0, string Nombre = "")
        {
            var Categoria = _service.Get(Id, Nombre);
            
            if (Categoria is null){
                return NotFound();
            }
            return Ok(Categoria);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(CategoriaRequest Categoria)
        {
            var newCategoria = _service.Create(Categoria);
            return CreatedAtAction(nameof(Get), new {Id = newCategoria.Idcategoria}, newCategoria);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update(int Id, CategoriaRequest Categoria)
        {
            if(Id != Categoria.Idcategoria)
            {
                return BadRequest();
            }
            var CategoriaToUpdate = _service.GetById(Id);

            if(CategoriaToUpdate is not null)
            {
                _service.Update(Id, Categoria);
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
            var CategoriaToDelete = _service.GetById(Id);

            if(CategoriaToDelete is not null)
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