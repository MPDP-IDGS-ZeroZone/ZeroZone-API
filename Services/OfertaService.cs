using System;
using ApiTienda.Data;
using ApiTienda.Data.Models;
using ApiTienda.Data.Request;
using ApiTienda.Data.Response;
namespace ApiTienda.Services;

public class OfertaService
{
    private readonly ProtibleDbContext _context;

    public OfertaService(ProtibleDbContext context)
    {
        _context = context;
    }

    public IEnumerable<OfertaResponse> Get(int Id = 0, int Producto = 0, DateTime? FechaInicio = null, DateTime? FechaCierre = null, bool ActivaOnly = false, int Page = 1, int PageSize = 10)
    {
        IQueryable<Oferta> ofertaQuery = _context.Ofertas.AsQueryable();

        if (Id != 0)
        {
            ofertaQuery = ofertaQuery.Where(o => o.Idoferta == Id);
        }

        if (Producto != 0)
        {
            ofertaQuery = ofertaQuery.Where(o => o.Idproducto == Producto);
        }

        if (FechaInicio != null)
        {
            ofertaQuery = ofertaQuery.Where(o => o.Fechainicio == FechaInicio);
        }

        if (FechaCierre != null)
        {
            ofertaQuery = ofertaQuery.Where(o => o.Fechacierre == FechaCierre);
        }

        if (ActivaOnly)
        {
            ofertaQuery = ofertaQuery.Where(o => o.Estatus == "Activa");
        }

        int skipAmount = (Page - 1) * PageSize;
        ofertaQuery = ofertaQuery.Skip(skipAmount).Take(PageSize);

        List<OfertaResponse> ofertas = ofertaQuery.Select(o => new OfertaResponse
        {
            Idoferta = o.Idoferta,
            Idproducto = o.Idproducto,
            Porcentaje = o.Porcentaje,
            Fechainicio = o.Fechainicio,
            Fechacierre = o.Fechacierre,
            Estatus = o.Estatus 
        }).ToList();

        return ofertas;
    }
    
    public Oferta GetById(int Id)
    {
        var Oferta = _context.Ofertas.Where(Oferta => Oferta.Idoferta == Id).First();
        return Oferta;
    }

    public MsgResponse Create(OfertaRequest newOferta)
    {
        Oferta Oferta = new Oferta();
        Oferta.Idproducto = newOferta.Idproducto;
        Oferta.Porcentaje = newOferta.Porcentaje;
        Oferta.Fechainicio = newOferta.Fechainicio;
        Oferta.Fechacierre = newOferta.Fechacierre;
        Oferta.Estatus = newOferta.Estatus;
        
        _context.Ofertas.Add(Oferta);
        _context.SaveChanges();

        return new MsgResponse{
            Id = Oferta.Idoferta,
            Msg = "oferta creada correctamente"
        };
    }

    public void Update(int Id, OfertaRequest Oferta)
    {
        var existingOferta = GetById(Id);

        if (existingOferta is not null)
        {
            existingOferta.Porcentaje = Oferta.Porcentaje;
            existingOferta.Fechainicio = Oferta.Fechainicio;
            existingOferta.Fechacierre = Oferta.Fechacierre;
            existingOferta.Estatus = Oferta.Estatus;

            _context.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        var OfertaToDelete = GetById(id);

        if (OfertaToDelete is not null)
        {
            OfertaToDelete.Estatus = "Eliminado";
            _context.SaveChanges();
        }
    }
}