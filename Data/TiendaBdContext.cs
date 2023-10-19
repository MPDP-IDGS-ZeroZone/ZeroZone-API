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

    public virtual DbSet<Oferta> Ofertas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Socio> Socios { get; set; }

    public virtual DbSet<UsuariosSocio> UsuariosSocios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Idcategoria).HasName("PK__Categori__140587C798EB184C");

            entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");
            entity.Property(e => e.Foto)
                .HasMaxLength(500)
                .HasColumnName("foto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(70)
                .HasColumnName("nombre");
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
                .HasMaxLength(500)
                .HasColumnName("foto");
            entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
