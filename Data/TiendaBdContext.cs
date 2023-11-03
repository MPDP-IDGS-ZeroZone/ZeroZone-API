using System;
using System.Collections.Generic;
using ApiTienda.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTienda.Data;

public partial class TiendaBdContext : DbContext
{
    public TiendaBdContext()
    {
    }

    public TiendaBdContext(DbContextOptions<TiendaBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<DetallesVentum> DetallesVenta { get; set; }

    public virtual DbSet<Key> Keys { get; set; }

    public virtual DbSet<Oferta> Ofertas { get; set; }

    public virtual DbSet<Plataforma> Plataformas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Socio> Socios { get; set; }

    public virtual DbSet<UsuariosSocio> UsuariosSocios { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Idcategoria).HasName("PK__Categori__140587C798EB184C");

            entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");
            entity.Property(e => e.Foto)
                .IsUnicode(false)
                .HasColumnName("foto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(70)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<DetallesVentum>(entity =>
        {
            entity.HasKey(e => e.Iddetalleventa).HasName("PK__Detalles__4EA18098FC6A9552");

            entity.Property(e => e.Iddetalleventa).HasColumnName("iddetalleventa");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.Idventa).HasColumnName("idventa");
            entity.Property(e => e.Preciounitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("preciounitario");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.DetallesVenta)
                .HasForeignKey(d => d.Idproducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallesV__idpro__45BE5BA9");

            entity.HasOne(d => d.IdventaNavigation).WithMany(p => p.DetallesVenta)
                .HasForeignKey(d => d.Idventa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallesV__idven__44CA3770");
        });

        modelBuilder.Entity<Key>(entity =>
        {
            entity.HasKey(e => e.Idkey).HasName("PK__Keys__07FFE172897A5713");

            entity.Property(e => e.Idkey).HasColumnName("idkey");
            entity.Property(e => e.Estatus)
                .HasMaxLength(70)
                .HasColumnName("estatus");
            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.Keyproducto)
                .HasMaxLength(255)
                .HasColumnName("keyproducto");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.Keys)
                .HasForeignKey(d => d.Idproducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Keys__idproducto__3F115E1A");
        });

        modelBuilder.Entity<Oferta>(entity =>
        {
            entity.HasKey(e => e.Idoferta).HasName("PK__Ofertas__A8E3A62A80BDCEC7");

            entity.Property(e => e.Idoferta).HasColumnName("idoferta");
            entity.Property(e => e.Estatus)
                .HasMaxLength(70)
                .HasColumnName("estatus");
            entity.Property(e => e.Fechacierre)
                .HasColumnType("date")
                .HasColumnName("fechacierre");
            entity.Property(e => e.Fechainicio)
                .HasColumnType("date")
                .HasColumnName("fechainicio");
            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.Porcentaje).HasColumnName("porcentaje");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.Oferta)
                .HasForeignKey(d => d.Idproducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ofertas__idprodu__0F624AF8");
        });

        modelBuilder.Entity<Plataforma>(entity =>
        {
            entity.HasKey(e => e.Idplataforma).HasName("PK__Platafor__F3C55CD8657E5AF0");

            entity.Property(e => e.Idplataforma).HasColumnName("idplataforma");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(70)
                .HasColumnName("nombre");
            entity.Property(e => e.UrlSitio)
                .HasMaxLength(255)
                .HasColumnName("url_sitio");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Idproducto).HasName("PK__Producto__DC53BE3C55C4E50A");

            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("date")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Foto)
                .IsUnicode(false)
                .HasColumnName("foto");
            entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");
            entity.Property(e => e.Idplataforma).HasColumnName("idplataforma");
            entity.Property(e => e.Idusuariosocio).HasColumnName("idusuariosocio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(70)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Statusp)
                .HasMaxLength(70)
                .HasColumnName("statusp");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.Tipo)
                .HasMaxLength(70)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdcategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Idcategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__idcat__0B91BA14");

            entity.HasOne(d => d.IdplataformaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Idplataforma)
                .HasConstraintName("FK__Productos__idpla__1F98B2C1");

            entity.HasOne(d => d.IdusuariosocioNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Idusuariosocio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__idusu__0C85DE4D");
        });

        modelBuilder.Entity<Socio>(entity =>
        {
            entity.HasKey(e => e.Idsocio).HasName("PK__Socios__F70B2B55A42BD761");

            entity.Property(e => e.Idsocio).HasColumnName("idsocio");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(70)
                .HasColumnName("apellidos");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(70)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<UsuariosSocio>(entity =>
        {
            entity.HasKey(e => e.Idusuariosocio).HasName("PK__Usuarios__7DB1FFC2E826FA47");

            entity.ToTable("Usuarios_socios");

            entity.Property(e => e.Idusuariosocio).HasColumnName("idusuariosocio");
            entity.Property(e => e.Idsocio).HasColumnName("idsocio");
            entity.Property(e => e.Mail)
                .HasMaxLength(50)
                .HasColumnName("mail");
            entity.Property(e => e.Pasword).HasColumnName("pasword");
            entity.Property(e => e.Rol)
                .HasMaxLength(20)
                .HasColumnName("rol");

            entity.HasOne(d => d.IdsocioNavigation).WithMany(p => p.UsuariosSocios)
                .HasForeignKey(d => d.Idsocio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuarios___idsoc__5EBF139D");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Idventa).HasName("PK__Ventas__F82D1AFBE960B8F8");

            entity.Property(e => e.Idventa).HasColumnName("idventa");
            entity.Property(e => e.Estatus)
                .HasMaxLength(70)
                .HasColumnName("estatus");
            entity.Property(e => e.Fechaventa)
                .HasColumnType("date")
                .HasColumnName("fechaventa");
            entity.Property(e => e.Idusuariosocio).HasColumnName("idusuariosocio");

            entity.HasOne(d => d.IdusuariosocioNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Idusuariosocio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ventas__idusuari__41EDCAC5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
