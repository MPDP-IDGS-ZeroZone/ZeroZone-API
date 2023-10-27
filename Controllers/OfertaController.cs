using Microsoft.AspNetCore.Mvc;
using ApiTienda.Services;
using ApiTienda.Data.Models;
using ApiTienda.Data.Request;
using Microsoft.AspNetCore.Authorization;

namespace ApiTienda.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OfertaController : Controller
    {
        private readonly OfertaService _service;
        public OfertaController(OfertaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Oferta>> Get(int Id = 0, int Producto = 0, DateTime? FechaInicio = null, DateTime? FechaCierre = null, bool ActivaOnly = false)
        {
            var Oferta = _service.Get(Id,Producto, FechaInicio, FechaCierre, ActivaOnly);
            
            if (Oferta is null){
                return NotFound();
            }
            return Ok(Oferta);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(OfertaRequest Oferta)
        {
            var newOferta = _service.Create(Oferta);
            return CreatedAtAction(nameof(Get), new {Id = newOferta.Idoferta}, newOferta);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update(int Id, OfertaRequest Oferta)
        {
            var OfertaToUpdate = _service.GetById(Id);

            if(OfertaToUpdate is not null)
            {
                _service.Update(Id, Oferta);
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
            var OfertaToDelete = _service.GetById(Id);

            if(OfertaToDelete is not null)
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