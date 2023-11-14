using ApiTienda.Data;
using ApiTienda.Data.Models;
using ApiTienda.Data.Request;
using ApiTienda.Data.Response;

namespace ApiTienda.Services
{
    public class PlataformaService
    {
        private readonly ProtibleDbContext _context;

        public PlataformaService(ProtibleDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PlataformaResponse> Get(int Id = 0, string Nombre = "", int Page = 1, int PageSize = 10)
        {
            IQueryable<Plataforma> plataformaQuery = _context.Plataformas.AsQueryable();

            if (Id != 0)
            {
                plataformaQuery = plataformaQuery.Where(p => p.Idplataforma == Id);
            }

            if (!string.IsNullOrEmpty(Nombre))
            {
                plataformaQuery = plataformaQuery.Where(p => p.Nombre.StartsWith(Nombre));
            }

            int skipAmount = (Page - 1) * PageSize;
            plataformaQuery = plataformaQuery.Skip(skipAmount).Take(PageSize);

            List<PlataformaResponse> plataformas = plataformaQuery.Select(p => new PlataformaResponse
            {
                Idplataforma = p.Idplataforma,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                UrlSitio = p.UrlSitio
            }).ToList();

            return plataformas;
        }

        public Plataforma GetById(int Id)
        {
            var plataforma = _context.Plataformas.Where(p => p.Idplataforma == Id).First();
            return plataforma;
        }

        public MsgResponse Create(PlataformaRequest newPlataforma)
        {
            Plataforma plataforma = new Plataforma();
            plataforma.Nombre = newPlataforma.Nombre;
            plataforma.Descripcion = newPlataforma.Descripcion;
            plataforma.UrlSitio = newPlataforma.UrlSitio;

            _context.Plataformas.Add(plataforma);
            _context.SaveChanges();

            return new MsgResponse
            {
                Id = plataforma.Idplataforma,
                Msg = "La plataforma se creo correctamente"
            };
        }

        public void Update(int Id, PlataformaRequest plataforma)
        {
            var existingPlataforma = GetById(Id);

            if (existingPlataforma != null)
            {
                existingPlataforma.Nombre = plataforma.Nombre;
                existingPlataforma.Descripcion = plataforma.Descripcion;
                existingPlataforma.UrlSitio = plataforma.UrlSitio;

                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var plataformaToDelete = GetById(id);

            if (plataformaToDelete != null)
            {
                _context.Plataformas.Remove(plataformaToDelete);
                _context.SaveChanges();
            }
        }
    }
}
