using Microsoft.AspNetCore.Mvc;
using ApiTienda.Services;
using ApiTienda.Data.Models;
using ApiTienda.Data.Request;
using Microsoft.AspNetCore.Authorization;
using TiendaAPI.Services;
using Microsoft.AspNetCore.Cors;
using ApiTienda.Data.Response;

namespace ApiTienda.Controllers
{
 
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : Controller
    {
        private readonly ProductoService _service;
        private readonly AuthService _auth;
        public ProductoController(ProductoService service, AuthService auth)
        {
            _service = service;
            _auth = auth;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Producto>> Get(int Id = 0, int SocioID = 0, string Nombre = "", int PrecioMin = 0, int PrecioMax = 99999, DateTime? FechaMin = null, DateTime? FechaMax = null, int Categoria = 0, string Tipo = "", string Statusp = "", int Page = 1, int PageSize = 10)
        {
            var producto = _service.Get(Id, SocioID, Nombre, PrecioMin, PrecioMax, FechaMin, FechaMax, Categoria, Tipo, Statusp, Page, PageSize);
            
            if (producto is null){
                return NotFound();
            }
            return Ok(producto);
        }
       
        [HttpPost]
        [Authorize]
        public IActionResult Create(ProductoRequest producto)
        {
            UsuariosSocioResponse usuariosSocio = new UsuariosSocioResponse();
            if (!string.IsNullOrEmpty(this.HttpContext.Request.Headers["Authorization"]) && this.HttpContext.Request.Headers["Authorization"].ToString().StartsWith("Bearer "))
            {
                string Token = this.HttpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length);
                usuariosSocio = _auth.FuncionMagica(Token);
            }
            
            if(usuariosSocio.Idusuariosocio != 0){
                var newProducto = _service.Create(producto, usuariosSocio.Idusuariosocio);
                return CreatedAtAction(nameof(Get), new {Id = newProducto.Idproducto}, newProducto);
            }else{
                return BadRequest(new{Error = "La funcion magica no funciono correctamente"});
            }
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update(int Id, ProductoRequest producto)
        {
            var ProductoToUpdate = _service.Get(Id).First();

            if(ProductoToUpdate is not null)
            {
                _service.Update(Id, producto);
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