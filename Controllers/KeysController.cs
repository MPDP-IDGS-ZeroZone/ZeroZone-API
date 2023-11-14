using Microsoft.AspNetCore.Mvc;
using ApiTienda.Services;
using ApiTienda.Data.Models;
using ApiTienda.Data.Request;
using Microsoft.AspNetCore.Authorization;

namespace ApiTienda.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeyController : ControllerBase
    {
        private readonly KeyService _service;

        public KeyController(KeyService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Key>> Get(int Id = 0, string Estatus = "", int Page = 1, int PageSize = 10)
        {
            var keys = _service.Get(Id, Estatus, Page, PageSize);

            if (keys is null)
            {
                return NotFound();
            }
            return Ok(keys);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(KeyRequest key)
        {
            if (key == null)
            {
                return BadRequest("Invalid model");
            }

            var newKey = _service.Create(key);
            return Ok(newKey);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update(int Id, KeyRequest key)
        {
            var keyToUpdate = _service.GetById(Id);

            if (keyToUpdate is not null)
            {
                _service.Update(Id, key);
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
            var keyToDelete = _service.GetById(Id);

            if (keyToDelete is not null)
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
