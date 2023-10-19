using System;
using TiendaApi.Data;
using TiendaApi.Data.Models;
using TiendaApi.Data.Request;
namespace TiendaApi.Services;

public class OfertaService
{
    private readonly TiendaBdContext _context;

    public OfertaService(TiendaBdContext context)
    {
        _context = context;
    }

    public IEnumerable<Oferta> Get(int Id = 0, int Producto = 0, DateTime? FechaInicio = null, DateTime? FechaCierre = null, bool ActivaOnly = false)
    {
        IEnumerable<Oferta> Oferta = _context.Ofertas.Select(Oferta => Oferta);
        
        if(Id != 0)
        {
            Oferta = Oferta.Where(Oferta => Oferta.Idoferta == Id);
        }

        if(Producto != 0)
        {
            Oferta = Oferta.Where(Oferta => Oferta.Idproducto == Producto);
        }

        if(FechaInicio != null)
        {
            Oferta = Oferta.Where(Oferta => Oferta.Fechainicio == FechaInicio);
        }

        if(FechaCierre != null)
        {
            Oferta = Oferta.Where(Oferta => Oferta.Fechacierre == FechaCierre);
        }

        if(ActivaOnly)
        {
            Oferta = Oferta.Where(Oferta => Oferta.Estatus == "Activa");
        }

        return Oferta;
    }
    
    public Oferta GetById(int Id)
    {
        var Oferta = _context.Ofertas.Where(Oferta => Oferta.Idoferta == Id).First();
        return Oferta;
    }

    public Oferta Create(OfertaRequest newOferta)
    {
        Oferta Oferta = new Oferta();
        Oferta.Idproducto = newOferta.Idproducto;
        Oferta.Porcentaje = newOferta.Porcentaje;
        Oferta.Fechainicio = newOferta.Fechainicio;
        Oferta.Fechacierre = newOferta.Fechacierre;
        Oferta.Estatus = newOferta.Estatus;
        
        _context.Ofertas.Add(Oferta);
        _context.SaveChanges();

        return Oferta;
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
            _context.Ofertas.Remove(OfertaToDelete);
            _context.SaveChanges();
        }
    }
}