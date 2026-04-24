using System;
using System.Collections.Generic;

namespace SistemadeAvisosEscolaresApi.Models.Entities;

public partial class Alumnos
{
    public int IdAlumno { get; set; }

    public string? Nombre { get; set; }

    public string? Matricula { get; set; }

    public string? Password { get; set; }

    public int? IdMaestro { get; set; }

    public virtual Maestros? IdMaestroNavigation { get; set; }
}
