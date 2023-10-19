using Microsoft.AspNetCore.Mvc;
using TiendaApi.Services;
using TiendaApi.Data.Models;
using TiendaApi.Data.Request;

namespace TiendaApi.Controllers
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
        [Route("")]
        public ActionResult<IEnumerable<Oferta>> Get(int Id = 0, int Producto = 0, DateTime? FechaInicio = null, DateTime? FechaCierre = null, bool ActivaOnly = false)
        {
            var Oferta = _service.Get(Id,Producto, FechaInicio, FechaCierre, ActivaOnly);
            
            if (Oferta is null){
                return NotFound();
            }
            return Ok(Oferta);
        }

        [HttpPost]
        public IActionResult Create(OfertaRequest Oferta)
        {
            var newOferta = _service.Create(Oferta);
            return CreatedAtAction(nameof(Get), new {Id = newOferta.Idoferta}, newOferta);
        }

        [HttpPut]
        public IActionResult Update(int Id, OfertaRequest Oferta)
        {
            if(Id != Oferta.Idoferta)
            {
                return BadRequest();
            }
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