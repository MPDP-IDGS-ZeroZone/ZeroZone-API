using Microsoft.AspNetCore.Mvc;
using ApiTienda.Services;
using ApiTienda.Data.Models;
using ApiTienda.Data.Request;
using Microsoft.AspNetCore.Authorization;
using TiendaAPI.Services;
using Microsoft.AspNetCore.Cors;

namespace ApiTienda.Controllers
{
 
    [ApiController]
    [Route("[controller]")]
    public class VentaController : Controller
    {
        private readonly VentaService _service;
        private readonly AuthService _auth;
        public VentaController(VentaService service, AuthService auth)
        {
            _service = service;
            _auth = auth;
        }
       
        [HttpPost]
        [Authorize]
        public IActionResult Create(VentaRequest newVenta)
        {
            UsuariosSocio usuariosSocio = new UsuariosSocio();
            if (!string.IsNullOrEmpty(this.HttpContext.Request.Headers["Authorization"]) && this.HttpContext.Request.Headers["Authorization"].ToString().StartsWith("Bearer "))
            {
                string Token = this.HttpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length);
                usuariosSocio = _auth.FuncionMagica(Token);
            }
            
            if(usuariosSocio.Idusuariosocio != 0){
                var Url = _service.createCheckoutSession(newVenta, usuariosSocio);
                
                //Response.Headers.Add("Location", Url);
                //return new StatusCodeResult(303);
                return Ok(Url);
            }else{
                return BadRequest(new{Error = "La funcion magica no funciono correctamente"});
            }
        }
    }
}