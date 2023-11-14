using ApiTienda.Data;
using ApiTienda.Data.Models;
using ApiTienda.Data.Request;
using ApiTienda.Data.Response;

namespace ApiTienda.Services;

public class CategoriaService
{
    private readonly ProtibleDbContext _context;

    public CategoriaService(ProtibleDbContext context)
    {
        _context = context;
    }

    public IEnumerable<CategoriaResponse> Get(int Id = 0, string Nombre = "", string Estatus = "", int Page = 1, int PageSize = 10)
    {
        IQueryable<Categoria> categoriaQuery = _context.Categorias.AsQueryable();

        if (Id != 0)
        {
            categoriaQuery = categoriaQuery.Where(c => c.Idcategoria == Id);
        }

        if (!string.IsNullOrEmpty(Nombre))
        {
            categoriaQuery = categoriaQuery.Where(c => c.Nombre.StartsWith(Nombre));
        }
        
        if (!string.IsNullOrEmpty(Estatus))
        {
            categoriaQuery = categoriaQuery.Where(c => c.Estatus == Estatus);
        }

        int skipAmount = (Page - 1) * PageSize;
        categoriaQuery = categoriaQuery.Skip(skipAmount).Take(PageSize);

        List<CategoriaResponse> categorias = categoriaQuery.Select(c => new CategoriaResponse
        {
            Idcategoria = c.Idcategoria,
            Nombre = c.Nombre,
            Foto = c.Foto
        }).ToList();

        return categorias;
    }
    
    public Categoria GetById(int Id)
    {
        var Categoria = _context.Categorias.Where(Categoria => Categoria.Idcategoria == Id).First();
        return Categoria;
    }

    public MsgResponse Create(CategoriaRequest newCategoria)
    {
        Categoria Categoria = new Categoria();
        Categoria.Nombre = newCategoria.Nombre;
        Categoria.Foto = newCategoria.Foto;
        
        _context.Categorias.Add(Categoria);
        _context.SaveChanges();

        return new MsgResponse{
            Id = Categoria.Idcategoria,
            Msg = "La categoria se creo correctamente"
        };
    }

    public void Update(int Id, CategoriaRequest Categoria)
    {
        var existingCategoria = GetById(Id);

        if (existingCategoria is not null)
        {
            existingCategoria.Nombre = Categoria.Nombre;
            existingCategoria.Foto = Categoria.Foto;

            _context.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        var CategoriaToDelete = GetById(id);

        if (CategoriaToDelete is not null)
        {
            CategoriaToDelete.Estatus = "Eliminado";
            _context.SaveChanges();
        }
    }
}