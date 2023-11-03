using System.Collections.Specialized;
using System.Data.Common;
using ApiTienda.Data;
using ApiTienda.Data.Models;
using ApiTienda.Data.Request;
using ApiTienda.Data.Response;
namespace ApiTienda.Services;

using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

using Stripe;
using Stripe.Checkout;

public class VentaService
{
    private readonly TiendaBdContext _context;

    public VentaService(TiendaBdContext context)
    {
        _context = context;
    }

    public string createCheckoutSession (VentaRequest newVenta, UsuariosSocio usuariosocio)
    {
        var detallesventa = newVenta.DetallesVenta;

        List<int> idProductos = new List<int>();
        foreach (var detalle in newVenta.DetallesVenta)
        {
            idProductos.Add(detalle.Idproducto);
        }

        IEnumerable<Producto> productos = _context.Productos.Where(p => idProductos.Contains(p.Idproducto));
        var DetallesVenta = (from detalle in detallesventa
                  join producto in _context.Productos on detalle.Idproducto equals producto.Idproducto
                  select new
                  {
                      Cantidad = detalle.cantidad,
                      Producto = producto
                  }).ToList();
        
        var domain = "http://localhost:4242";

        var lineItems = new List<SessionLineItemOptions>();
        foreach (var detalles in DetallesVenta)
        {
            var lineItem = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = detalles.Producto.Nombre,
                        Description = detalles.Producto.Descripcion,
                        Images = new List<string> {detalles.Producto.Foto ?? ""},
                        Metadata = new Dictionary<string, string>
                        {
                            {"Idproducto", detalles.Producto.Idproducto.ToString()}
                        },
                    },
                    Currency = "MXN",
                    UnitAmount = (long)(detalles.Producto.Precio * 100),
                },
                Quantity = detalles.Cantidad,
            };

            lineItems.Add(lineItem);
        }

        var options = new SessionCreateOptions
        {
            LineItems = lineItems, // Agregar la lista de line items creada
            Mode = "payment",
            SuccessUrl = domain + "/success",
            CancelUrl = domain + "/cancel",
            CustomerEmail = usuariosocio.Mail,
            Metadata = new Dictionary<string, string>
            {
                {"Idusuariosocio", usuariosocio.Idusuariosocio.ToString()}
            }
        };

        var service = new SessionService();
        Session session = service.Create(options);

        return session.Url;

    }

}