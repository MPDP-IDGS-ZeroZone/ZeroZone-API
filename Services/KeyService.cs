using System;
using System.Collections.Generic;
using ApiTienda.Data;
using ApiTienda.Data.Models;
using ApiTienda.Data.Request;
using ApiTienda.Data.Response;

namespace ApiTienda.Services
{
    public class KeyService
    {
        private readonly ProtibleDbContext _context;

        public KeyService(ProtibleDbContext context)
        {
            _context = context;
        }

        public IEnumerable<KeyResponse> Get(int Id = 0, int IdProducto = 0, string Estatus = "", int Page = 1, int PageSize = 10)
        {
            IQueryable<Key> keyQuery = _context.Keys.AsQueryable();

            if (Id != 0)
            {
                keyQuery = keyQuery.Where(k => k.Idkey == Id);
            }

            if (IdProducto != 0)
            {
                keyQuery = keyQuery.Where(k => k.Idproducto == IdProducto);
            }

            if (!string.IsNullOrEmpty(Estatus))
            {
                keyQuery = keyQuery.Where(k => k.Estatus == Estatus);
            }

            int skipAmount = (Page - 1) * PageSize;
            keyQuery = keyQuery.Skip(skipAmount).Take(PageSize);

            List<KeyResponse> plataformas = keyQuery.Select(p => new KeyResponse
            {
                Idkey = p.Idkey,
                Idproducto = p.Idproducto,
                Keyproducto = p.Keyproducto,
                Estatus = p.Estatus
            }).ToList();

            return plataformas;
        }

        public Key GetById(int Id)
        {
            var key = _context.Keys.Where(p => p.Idkey == Id).First();;
            return key;
        }

        public MsgResponse Create(KeyRequest newKey)
        {
            Key key = new Key();
            key.Idproducto = newKey.Idproducto;
            key.Keyproducto = newKey.Keyproducto;
            key.Estatus = newKey.Estatus;

            _context.Keys.Add(key);
            _context.SaveChanges();

            return new MsgResponse
            {
                Id = key.Idkey,
                Msg = "La clave se creó correctamente"
            };
        }

        public void Update(int Id, KeyRequest key)
        {
            var existingKey = GetById(Id);

            if (existingKey != null)
            {
                existingKey.Idproducto = key.Idproducto;
                existingKey.Keyproducto = key.Keyproducto;
                existingKey.Estatus = key.Estatus;

                _context.SaveChanges();
            }
        }

        public void Delete(int Id)
        {
            var keyToDelete = GetById(Id);

            if (keyToDelete != null)
            {
                keyToDelete.Estatus = "Eliminado";
                _context.SaveChanges();
            }
        }
    }
}
