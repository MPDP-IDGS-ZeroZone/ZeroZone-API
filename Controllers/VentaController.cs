using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using ApiTienda.Services;
using ApiTienda.Data.Models;
using ApiTienda.Data.Request;
using Microsoft.AspNetCore.Authorization;
using TiendaAPI.Services;
using Microsoft.AspNetCore.Cors;
using Stripe;
using Stripe.Checkout;
using System.ComponentModel.Design;

namespace ApiTienda.Controllers
{
 
    [ApiController]
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
        [Route("[controller]")]
        public async Task<IActionResult> Create(VentaRequest newVenta)
        {
            UsuariosSocio usuariosSocio = new UsuariosSocio();
            if (!string.IsNullOrEmpty(this.HttpContext.Request.Headers["Authorization"]) && this.HttpContext.Request.Headers["Authorization"].ToString().StartsWith("Bearer "))
            {
                string Token = this.HttpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length);
                usuariosSocio = _auth.FuncionMagica(Token);
            }
            
            if(usuariosSocio.Idusuariosocio != 0){
                var Url = await _service.createCheckoutSession(newVenta, usuariosSocio);
                
                //Response.Headers.Add("Location", Url);
                //return new StatusCodeResult(303);
                return Ok(Url);
            }else{
                return BadRequest(new{Error = "La funcion magica no funciono correctamente"});
            }
        }

        [HttpPost]
        [Route("webhook")]
        public async Task<IActionResult> Index()
        {
            try
            {
                const string secret = "whsec_b2cf9f62cc70012836b45aba4b0ff2791f642b198f387c771fb0810ac1a74b75";
                var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    secret
                );

                Session session;

                switch (stripeEvent.Type)
                {
                    case Events.CheckoutSessionCompleted:
                        session = stripeEvent.Data.Object as Session ?? new Session();

                        if (session.PaymentStatus == "paid") {
                            await _service.FulfillOrderAsync(session, "Completado");
                        }else{
                            await _service.FulfillOrderAsync(session, "Cancelado");
                        }

                        break;

                    case Events.CheckoutSessionExpired:
                        session = stripeEvent.Data.Object as Session ?? new Session();
                        await _service.FulfillOrderAsync(session, "Cancelado");

                        break;

                    case Events.CheckoutSessionAsyncPaymentSucceeded:
                        session = stripeEvent.Data.Object as Session ?? new Session();
                        await _service.FulfillOrderAsync(session,"Completado");
                        
                        break;

                    case Events.CheckoutSessionAsyncPaymentFailed:
                        session = stripeEvent.Data.Object as Session ?? new Session();
                        await _service.FulfillOrderAsync(session, "Cancelado");

                        _service.EmailCustomerAboutFailedPayment(session);
                        
                        break;
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest(e);
            }
        }
    }
}