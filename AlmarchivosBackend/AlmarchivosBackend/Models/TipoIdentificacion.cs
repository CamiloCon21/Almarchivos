using System;
using System.Collections.Generic;

namespace AlmarchivosBackend.Models;

public partial class TipoIdentificacion
{
    public int IdTi { get; set; }

    public string? TipoIdentificacion1 { get; set; }

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
