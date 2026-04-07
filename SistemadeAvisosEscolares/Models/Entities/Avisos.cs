using System;
using System.Collections.Generic;

namespace SistemadeAvisosEscolares.Models.Entities;

public partial class Avisos
{
    public int IdAviso { get; set; }

    public string? Titulo { get; set; }

    public string? Contenido { get; set; }

    public int? IdMaestro { get; set; }

    public string? TipoAviso { get; set; }

    public DateTime? FechaEnvio { get; set; }

    public DateTime? FechaCaducidad { get; set; }

    public int? IdAlumno { get; set; }
}
