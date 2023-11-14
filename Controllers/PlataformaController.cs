using Microsoft.AspNetCore.Mvc;
using ApiTienda.Services;
using ApiTienda.Data.Models;
using ApiTienda.Data.Request;
using Microsoft.AspNetCore.Authorization;

namespace ApiTienda.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlataformaController : Controller
    {
        private readonly PlataformaService _service;

        public PlataformaController(PlataformaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Plataforma>> Get(int Id = 0, string Nombre = "", int Page = 1, int PageSize = 10)
        {
            var plataformas = _service.Get(Id, Nombre, Page, PageSize);

            if (plataformas is null)
            {
                return NotFound();
            }
            return Ok(plataformas);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(PlataformaRequest plataforma)
        {
            var newPlataforma = _service.Create(plataforma);
            return Ok(newPlataforma);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update(int Id, PlataformaRequest plataforma)
        {
            var plataformaToUpdate = _service.GetById(Id);

            if (plataformaToUpdate is not null)
            {
                _service.Update(Id, plataforma);
                return Ok();
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
            var plataformaToDelete = _service.GetById(Id);

            if (plataformaToDelete is not null)
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
