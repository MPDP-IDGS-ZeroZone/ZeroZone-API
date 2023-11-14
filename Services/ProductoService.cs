using ApiTienda.Data;
using ApiTienda.Data.Models;
using ApiTienda.Data.Request;
using ApiTienda.Data.Response;
namespace ApiTienda.Services;

public class ProductoService
{
    private readonly ProtibleDbContext _context;

    public ProductoService(ProtibleDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ProductoResponse> Get(int Id = 0, int SocioID = 0, string Nombre = "", int PrecioMin = 0, int PrecioMax = 99999, DateTime? FechaMin = null, DateTime? FechaMax = null, int Categoria = 0, string Tipo = "", string Statusp = "", int Page = 1, int PageSize = 10)
        {
        IQueryable<Producto> productoQuery = _context.Productos.AsQueryable();

        if (Id != 0)
        {
            productoQuery = productoQuery.Where(p => p.Idproducto == Id);
        }

        if (SocioID != 0)
        {
            productoQuery = productoQuery.Where(p => p.Idusuariosocio == SocioID);
        }

        if (!string.IsNullOrEmpty(Nombre))
        {
            productoQuery = productoQuery.Where(p => p.Nombre.ToLower().StartsWith(Nombre.ToLower())).OrderBy(p => p.Nombre);
        }

        productoQuery = productoQuery.Where(p => p.Precio >= PrecioMin && p.Precio <= PrecioMax);

        if (FechaMin != null && FechaMax != null)
        {
            productoQuery = productoQuery.Where(p => p.FechaCreacion >= FechaMin && p.FechaCreacion <= FechaMax);
        }

        if (Categoria != 0)
        {
            productoQuery = productoQuery.Where(p => p.Idcategoria == Categoria);
        }

        if (!string.IsNullOrEmpty(Tipo))
        {
            productoQuery = productoQuery.Where(p => p.Tipo == Tipo);
        }

        if (!string.IsNullOrEmpty(Statusp))
        {
            productoQuery = productoQuery.Where(p => p.Statusp == Statusp);
        }

        int skipAmount = (Page - 1) * PageSize;
        productoQuery = productoQuery.Skip(skipAmount).Take(PageSize);

        List<ProductoResponse> response = productoQuery.Select(p => new ProductoResponse
        {
            Idproducto = p.Idproducto,
            Idusuariosocio = p.Idusuariosocio,
            Idplataforma = p.Idplataforma,
            Socio = p.IdusuariosocioNavigation.IdsocioNavigation,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion,
            Precio = p.Precio,
            Foto = p.Foto ?? "",
            FechaCreacion = p.FechaCreacion,
            Categoria = _context.Categorias.Where(Categoria => Categoria.Idcategoria == p.Idcategoria).Select(c => new CategoriaResponse
            {
                Idcategoria = c.Idcategoria,
                Nombre = c.Nombre,
                Foto = c.Foto
            }).First(),
            Tipo = p.Tipo,
            Stock = p.Stock,
            Statusp = p.Statusp,
            Oferta = p.Oferta.Where(Oferta => Oferta.Estatus == "Activa" && Oferta.Fechainicio <= DateTime.Now && Oferta.Fechacierre >= DateTime.Now).ToList(),
        }).ToList();

        return response;
    }
    
    public Producto GetById(int Id)
    {
        var producto = _context.Productos.Where(producto => producto.Idproducto == Id).First();
        return producto;
    }

    public MsgResponse Create(ProductoRequest newProducto, int idUsuarioSocio)
    {

        Producto producto = new Producto{
            Idusuariosocio = idUsuarioSocio,
            Nombre = newProducto.Nombre,
            Descripcion = newProducto.Descripcion,
            Precio = newProducto.Precio,
            Foto = newProducto.Foto,
            FechaCreacion = DateTime.Now,
            Idcategoria = newProducto.IdCategoria,
            Tipo = newProducto.Tipo,
            Stock = newProducto.Stock,
            Statusp = newProducto.Statusp,
            Idplataforma = newProducto.Idplataforma,
        };
        
        _context.Productos.Add(producto);
        _context.SaveChanges();

        foreach (var key in newProducto.Keys)
        {
            var nuevaKey = new Key
            {
                Idproducto = producto.Idproducto,
                Keyproducto = key,
                Estatus = "Disponible",
            };
            _context.Keys.Add(nuevaKey);
        }
        _context.SaveChanges();

        var Response = new MsgResponse {
            Id = producto.Idproducto,
            Msg = "El producto se creo correctamente"
        };
        
        return Response;
    }

    public void Update(int Id, ProductoRequest producto)
    {
        var existingProducto = GetById(Id);

        if (existingProducto is not null)
        {
            existingProducto.Nombre = producto.Nombre;
            existingProducto.Descripcion = producto.Descripcion;
            existingProducto.Precio = producto.Precio;
            existingProducto.Foto = producto.Foto;
            existingProducto.Idcategoria = producto.IdCategoria;
            existingProducto.Tipo = producto.Tipo;
            existingProducto.Stock = producto.Stock;
            existingProducto.Statusp = producto.Statusp;

            _context.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        var ProductoToDelete = GetById(id);

        if (ProductoToDelete is not null)
        {
            ProductoToDelete.Statusp = "Eliminado";
            _context.SaveChanges();
        }
    }
}