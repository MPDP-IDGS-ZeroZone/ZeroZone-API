using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class TiendaDbContext : DbContext
    {
        public TiendaDbContext(DbContextOptions<TiendaDbContext> options) : base (options) {}
        public DbSet<Producto> Productos { get; set; }
        public DbSet<UsuarioSocio> UsuarioSocios { get; set; }
        public DbSet<Socios> Socios { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carrito>()
                .HasKey(c => new { c.IdCarrito, c.IdCliente, c.IdProducto });

            modelBuilder.Entity<Cliente>()
                .HasKey(c => c.IdCliente);

            modelBuilder.Entity<Producto>()
                .HasKey(p => p.IdProducto);

            modelBuilder.Entity<Socios>()
                .HasKey(s => s.IdSocio);

            modelBuilder.Entity<UsuarioSocio>()
                .HasKey(u => u.IdUsuarioSocio);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.UsuarioSocio)
                .WithMany(u => u.Productos)
                .HasForeignKey(p => p.IdUsuarioSocio)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UsuarioSocio>()
                .HasOne(u => u.Socio)
                .WithMany(s => s.UsuariosSocios)
                .HasForeignKey(u => u.IdSocio)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Carrito>()
                .HasOne(c => c.Cliente)
                .WithMany(c => c.Carritos)
                .HasForeignKey(c => c.IdCliente)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Carrito>()
                .HasOne(c => c.Producto)
                .WithMany(p => p.Carritos)
                .HasForeignKey(c => c.IdProducto)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración para las propiedades Monto y Precio
            modelBuilder.Entity<Carrito>()
                .Property(c => c.Monto)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasColumnType("decimal(10,2)");

            base.OnModelCreating(modelBuilder);
        }

    }
}