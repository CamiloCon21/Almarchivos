using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AlmarchivosBackend.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Usuario1 { get; set; }

    public string? Contraseña { get; set; }

    public string? FechaCreacion { get; set; }

    public int? IdPersona { get; set; }

    [JsonIgnore]
    public virtual Persona? IdPersonaNavigation { get; set; }
}
