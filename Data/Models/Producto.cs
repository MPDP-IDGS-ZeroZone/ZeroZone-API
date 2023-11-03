using System;
using System.Collections.Generic;

namespace ApiTienda.Data.Models;

public partial class Producto
{
    public int Idproducto { get; set; }

    public int Idusuariosocio { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public decimal Precio { get; set; }

    public string? Foto { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int Idcategoria { get; set; }

    public string Tipo { get; set; } = null!;

    public int Stock { get; set; }

    public string Statusp { get; set; } = null!;

    public int? Idplataforma { get; set; }

    public virtual ICollection<DetallesVentum> DetallesVenta { get; set; } = new List<DetallesVentum>();

    public virtual Categoria IdcategoriaNavigation { get; set; } = null!;

    public virtual Plataforma? IdplataformaNavigation { get; set; }

    public virtual UsuariosSocio IdusuariosocioNavigation { get; set; } = null!;

    public virtual ICollection<Key> Keys { get; set; } = new List<Key>();

    public virtual ICollection<Oferta> Oferta { get; set; } = new List<Oferta>();
}
