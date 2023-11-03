using System.Collections.Specialized;
using System.Data.Common;
using ApiTienda.Data;
using ApiTienda.Data.Models;
using ApiTienda.Data.Request;
using ApiTienda.Data.Response;
namespace ApiTienda.Services;

using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

public class VentaService
{
    private readonly TiendaBdContext _context;

    public VentaService(TiendaBdContext context)
    {
        _context = context;
    }

    public async Task<string> createCheckoutSession (VentaRequest newVenta, UsuariosSocio usuariosocio)
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
        
        var domain = "http://localhost:5240";

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

        DateTime currentDateTime = DateTime.UtcNow;
        DateTime expirationTime = currentDateTime.AddMinutes(30);

        options.ExpiresAt = expirationTime;

        var service = new SessionService();
        Session session = service.Create(options);

        await CreateVentaAsync(usuariosocio.Idusuariosocio, session.Id);
        foreach (var detalles in DetallesVenta)
        {
            await CreateDetallesVentasAsync(detalles.Producto.Idproducto, detalles.Cantidad, detalles.Producto.Precio, session.Id);
        }

        return session.Url;
    }
    public void EmailCustomerAboutFailedPayment(Session session) {
      // TODO: fill me in
    }

    public async Task FulfillOrderAsync(Session session, string status) {
        string idStripe = session.Id;
        var venta = await _context.Ventas.FirstAsync(v => v.Idstripe == idStripe);

        if (venta != null)
        {
            venta.Estatus = status;
            await _context.SaveChangesAsync();
        }
    }

    public async Task CreateVentaAsync(int Idusuariosocio, string Idstripe) {
        var nuevaVenta = new Venta
        {
            Idstripe = Idstripe,
            Idusuariosocio = Idusuariosocio,
            Fechaventa = DateTime.Now,
            Estatus = "Pendiente"
        };

        _context.Ventas.Add(nuevaVenta);
        await _context.SaveChangesAsync();
    }

    public async Task CreateDetallesVentasAsync(int idProducto, int cantidad, decimal precioUnitario, string idStripe) {
        var nuevaVenta = await _context.Ventas.FirstAsync(v => v.Idstripe == idStripe);
        int idVentaGenerado = nuevaVenta.Idventa;

        var detalleVenta = new DetallesVentum
        {
            Idventa = idVentaGenerado,
            Idproducto = idProducto,
            Cantidad = cantidad,
            Preciounitario = precioUnitario
        };

        _context.DetallesVenta.Add(detalleVenta);
        await _context.SaveChangesAsync();
    }
}