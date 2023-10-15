using ApiTienda.Data;
using ApiTienda.Data.Models;
using ApiTienda.Request;

namespace ApiTienda.Services;

public class ProductoService
{
    private readonly TiendaBdContext _context;

    public ProductoService(TiendaBdContext context)
    {
        _context = context;
    }

    public IEnumerable<Producto> Get()
    {
        var producto = _context.Productos.Where(producto => producto.Statusp == "Disponible");
        return producto;
    }

    public IEnumerable<Producto> GetById(int id)
    {
        var producto = _context.Productos.Where(producto => producto.Idproducto == id && producto.Statusp == "Disponible");
        return producto;
    }

    public IEnumerable<Producto> GetByCategory(string categoria)
    {
        var producto = _context.Productos.Where(producto => producto.Categoria == categoria && producto.Statusp == "Disponible");
        return producto;
    }

    public IEnumerable<Producto> GetBySocio(int SocioID)
    {
        var producto = _context.Productos.Where(producto => producto.Idusuariosocio == SocioID);
        return producto;
    }

    public IEnumerable<Producto> SearchByNombre(string Nombre)
    {
        var producto = _context.Productos.Where(producto => producto.Nombre.StartsWith(Nombre) && producto.Statusp == "Disponible").OrderBy(producto => producto.Nombre);
        return producto;
    }
    
    public Producto Create(ProductoRequest newProducto)
    {
        Producto producto = new Producto();
        producto.Nombre = newProducto.Nombre;
        producto.Precio = newProducto.Precio;
        producto.Descripcion = newProducto.Descripcion;
        producto.Stock = newProducto.Stock;
        producto.Categoria = newProducto.Categoria;
        producto.Statusp = newProducto.Statusp;
        producto.Idusuariosocio = newProducto.Idusuariosocio;

        _context.Productos.Add(producto);
        _context.SaveChanges();

        return producto;
    }

    public void Update(int Id, ProductoRequest producto)
    {
        var existingProducto = GetById(Id).First();

        if (existingProducto is not null)
        {
            existingProducto.Nombre = producto.Nombre;
            existingProducto.Precio = producto.Precio;
            existingProducto.Descripcion = producto.Descripcion;
            existingProducto.Stock = producto.Stock;
            existingProducto.Categoria = producto.Categoria;
            existingProducto.Statusp = producto.Statusp;

            _context.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        var ProductoToDelete = GetById(id).First();

        if (ProductoToDelete is not null)
        {
            _context.Productos.Remove(ProductoToDelete);
            _context.SaveChanges();
        }
    }
}