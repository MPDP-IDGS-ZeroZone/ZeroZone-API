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

    public IEnumerable<ProductoResponse> Get(int Id = 0, int Categoria = 0, int SocioID = 0, string Nombre = "", bool DisponibleOnly = false)
    {
        IEnumerable<Producto> producto = _context.Productos.Select(producto => producto);
        
        if(Id != 0)
        {
            producto = producto.Where(producto => producto.Idproducto == Id);
        }
        
        if(Categoria != 0)
        {
            producto = producto.Where(producto => producto.Idcategoria == Categoria);
        }
        
        if(SocioID != 0)
        {
            producto = producto.Where(producto => producto.Idusuariosocio == SocioID);
        }
        
        if(Nombre != "")
        {
            producto = producto.Where(producto => producto.Nombre.StartsWith(Nombre)).OrderBy(producto => producto.Nombre);
        }
        
        if(DisponibleOnly)
        {
            producto = producto.Where(producto => producto.Statusp == "Disponible");
        }

        List<ProductoResponse> response = new List<ProductoResponse>();
        
        var categorias = _context.Categorias.ToList();
        var usuariosSocios = _context.UsuariosSocios.ToList();
        var socios = _context.Socios.ToList();
        var ofertas = _context.Ofertas.Where(Oferta => Oferta.Estatus == "Activa" && Oferta.Fechainicio <= DateTime.Now && Oferta.Fechacierre >= DateTime.Now).ToList();

        foreach(Producto p in producto)
        {
            ProductoResponse newProducto = new ProductoResponse();
            var categoria = categorias.First(Categoria => Categoria.Idcategoria == p.Idcategoria);
            var usuariosSocio = usuariosSocios.First(usuariosSocio => usuariosSocio.Idusuariosocio == p.Idusuariosocio);
            var socio = socios.First(socio => socio.Idsocio == usuariosSocio.Idsocio);
            var oferta = ofertas.Where(Oferta => Oferta.Idproducto == p.Idproducto);

            newProducto.Idproducto = p.Idproducto;
            newProducto.Idusuariosocio = p.Idusuariosocio;
            newProducto.Socio = socio;
            newProducto.Nombre = p.Nombre;
            newProducto.Descripcion = p.Descripcion;
            newProducto.Precio = p.Precio;
            newProducto.Foto = p.Foto;
            newProducto.FechaCreacion = p.FechaCreacion;
            newProducto.Categoria = categoria;
            newProducto.Tipo = p.Tipo;
            newProducto.Stock = p.Stock;
            newProducto.Statusp = p.Statusp;
            newProducto.Oferta = oferta;

            response.Add(newProducto);
        };

        return response;
    }
    
    public Producto GetById(int Id)
    {
        var producto = _context.Productos.Where(producto => producto.Idproducto == Id).First();
        return producto;
    }

    public Producto Create(ProductoRequest newProducto, int idSocio)
    {
        Producto producto = new Producto();
        producto.Idusuariosocio = idSocio;
        producto.Nombre = newProducto.Nombre;
        producto.Descripcion = newProducto.Descripcion;
        producto.Precio = newProducto.Precio;
        producto.Foto = newProducto.Foto;
        producto.FechaCreacion = newProducto.FechaCreacion;
        producto.Idcategoria = newProducto.IdCategoria;
        producto.Tipo = newProducto.Tipo;
        producto.Stock = newProducto.Stock;
        producto.Statusp = newProducto.Statusp;
        
        _context.Productos.Add(producto);
        _context.SaveChanges();

        return producto;
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
            existingProducto.FechaCreacion = producto.FechaCreacion;
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