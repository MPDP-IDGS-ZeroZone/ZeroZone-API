using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTienda.Data.Request;
using ApiTienda.Data.Response;
using Microsoft.AspNetCore.Mvc;
using TiendaAPI.Services;
using Microsoft.AspNetCore.Authorization;
using ApiTienda.Data.Models;

namespace TiendaAPI.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AuthService auth)
        {
            _auth = auth;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(Login model)
        {
            var token = _auth.Authenticate(model.Mail, model.Pasword);

            if (token == null)
            {
                return Unauthorized(); // Credenciales no válidas
            }

            return Ok(token);
        }

        [HttpGet]
        [Authorize]
        [Route("Account")]
        public ActionResult<IEnumerable<AccountResponse>> Get(int Id = 0, string Estatus = "", int Page = 1, int PageSize = 10)
        {
            var AccountResponse = _auth.Get(Id, Estatus, Page, PageSize);
            
            if (AccountResponse is null){
                return NotFound();
            }
            return Ok(AccountResponse);
        }

        [HttpPost]
        [Route("Account")]
        public IActionResult CreateAccount(Account Account)
        {
            if(_auth.EmailExist(Account.Mail)){
                return BadRequest("Este correo ya esta registrado");
            } else{
                var newAccount = _auth.Create(Account);
                return CreatedAtAction(nameof(Get), new {Id = newAccount.Idsocio}, newAccount);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("Account")]
        public IActionResult Update(int Id, Account Account)
        {
            var AccountToUpdate = _auth.GetById(Id);

            if(AccountToUpdate is not null)
            {
                _auth.Update(Id, Account);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("Account")]
        public IActionResult Delete(int Id)
        {
            var socioToDelete = _auth.GetById(Id);

            if(socioToDelete is not null)
            {
                _auth.Delete(Id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("MyAccount")]
        public ActionResult<IEnumerable<AccountResponse>> GetMyAccount()
        {
            UsuariosSocioResponse usuariosSocio = new UsuariosSocioResponse();
            if (!string.IsNullOrEmpty(this.HttpContext.Request.Headers["Authorization"]) && this.HttpContext.Request.Headers["Authorization"].ToString().StartsWith("Bearer "))
            {
                string Token = this.HttpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length);
                usuariosSocio = _auth.FuncionMagica(Token);
            }
            
            if(usuariosSocio.Idsocio != 0)
            {
                var AccountResponse = _auth.Get(usuariosSocio.Idsocio);
            
                if (AccountResponse is null){
                    return NotFound();
                }
                return Ok(AccountResponse);
            }else
            {
                return BadRequest(new{Error = "La funcion magica no funciono correctamente"});
            }
        }

        [HttpPut]
        [Authorize]
        [Route("MyAccount")]
        public IActionResult UpdateMyAccount(Account Account)
        {
            UsuariosSocioResponse usuariosSocio = new UsuariosSocioResponse();
            if (!string.IsNullOrEmpty(this.HttpContext.Request.Headers["Authorization"]) && this.HttpContext.Request.Headers["Authorization"].ToString().StartsWith("Bearer "))
            {
                string Token = this.HttpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length);
                usuariosSocio = _auth.FuncionMagica(Token);
            }
            
            if(usuariosSocio.Idsocio != 0)
            {
                var AccountToUpdate = _auth.GetById(usuariosSocio.Idsocio);

                if(AccountToUpdate is not null)
                {
                    _auth.Update(usuariosSocio.Idsocio, Account);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }else
            {
                return BadRequest(new{Error = "La funcion magica no funciono correctamente"});
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("MyAccount")]
        public IActionResult DeleteMyAccount()
        {
            UsuariosSocioResponse usuariosSocio = new UsuariosSocioResponse();
            if (!string.IsNullOrEmpty(this.HttpContext.Request.Headers["Authorization"]) && this.HttpContext.Request.Headers["Authorization"].ToString().StartsWith("Bearer "))
            {
                string Token = this.HttpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length);
                usuariosSocio = _auth.FuncionMagica(Token);
            }
            
            if(usuariosSocio.Idsocio != 0)
            {
                var socioToDelete = _auth.GetById(usuariosSocio.Idsocio);

                if(socioToDelete is not null)
                {
                    _auth.Delete(usuariosSocio.Idsocio);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }else
            {
                return BadRequest(new{Error = "La funcion magica no funciono correctamente"});
            }
        }
    }
}