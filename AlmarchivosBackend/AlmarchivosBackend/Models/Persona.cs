using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace AlmarchivosBackend.Models;

public partial class Persona
{
    [Key]
    public int IdPersona { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public long? NumeroIdentificacion { get; set; }

    public string? Email { get; set; }

    public int? IdTipoId { get; set; }

    public string? FechaCreacion { get; set; }

    [JsonIgnore]
    public virtual TipoIdentificacion? IdTipo { get; set; }

    [JsonIgnore]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
