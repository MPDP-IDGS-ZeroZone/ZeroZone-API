using ApiTienda.Data;
using ApiTienda.Data.Models;
using ApiTienda.Data.Request;
using ApiTienda.Data.Response;
namespace ApiTienda.Services;

public class ProductoService
{
    private readonly TiendaBdContext _context;

    public ProductoService(TiendaBdContext context)
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

    public ProductoResponse Create(ProductoRequest newProducto, int idUsuarioSocio)
    {

        Producto producto = new Producto();
        producto.Idusuariosocio = idUsuarioSocio;
        producto.Nombre = newProducto.Nombre;
        producto.Descripcion = newProducto.Descripcion;
        producto.Precio = newProducto.Precio;
        producto.Foto = newProducto.Foto;
        producto.FechaCreacion = DateTime.Now;
        producto.Idcategoria = newProducto.IdCategoria;
        producto.Tipo = newProducto.Tipo;
        producto.Stock = newProducto.Stock;
        producto.Statusp = newProducto.Statusp;
        
        _context.Productos.Add(producto);
        _context.SaveChanges();

        UsuariosSocio usuariosSocio = _context.UsuariosSocios.Where(UsuariosSocio => UsuariosSocio.Idusuariosocio == idUsuarioSocio).First();
        CategoriaResponse categoria = _context.Categorias.Where(Categoria => Categoria.Idcategoria == producto.Idcategoria).Select(c => new CategoriaResponse
        {
            Idcategoria = c.Idcategoria,
            Nombre = c.Nombre,
            Foto = c.Foto
        }).First();

        ProductoResponse response = new ProductoResponse
        {
            Socio = usuariosSocio.IdsocioNavigation,
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio,
            Foto = producto.Foto,
            FechaCreacion = producto.FechaCreacion,
            Categoria = categoria,
            Tipo = producto.Tipo,
            Stock = producto.Stock,
            Statusp = producto.Statusp
        };
        
        return response;
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
            _context.Productos.Remove(ProductoToDelete);
            _context.SaveChanges();
        }
    }
}