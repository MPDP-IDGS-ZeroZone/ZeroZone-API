using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiTienda.Data.Models;

public partial class Producto
{
    public int Idproducto { get; set; }

    public int Idusuariosocio { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public decimal Precio { get; set; }

    public string Foto { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public int Idcategoria { get; set; }

    public string Tipo { get; set; } = null!;

    public int Stock { get; set; }

    public string Statusp { get; set; } = null!;

    [JsonIgnore]
    public virtual Categoria IdcategoriaNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual UsuariosSocio IdusuariosocioNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Oferta> Oferta { get; set; } = new List<Oferta>();
}
