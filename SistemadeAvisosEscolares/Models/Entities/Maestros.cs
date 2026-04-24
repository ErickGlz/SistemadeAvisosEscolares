using System;
using System.Collections.Generic;

namespace SistemadeAvisosEscolaresApi.Models.Entities;

public partial class Maestros
{
    public int IdMaestro { get; set; }

    public string? Nombre { get; set; }

    public string? Password { get; set; }

    public string? Grupo { get; set; }

    public virtual ICollection<Alumnos> Alumnos { get; set; } = new List<Alumnos>();
}
