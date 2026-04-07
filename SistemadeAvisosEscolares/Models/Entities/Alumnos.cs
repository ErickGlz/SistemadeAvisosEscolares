using System;
using System.Collections.Generic;

namespace SistemadeAvisosEscolares.Models.Entities;

public partial class Alumnos
{
    public int IdAlumno { get; set; }

    public string? Nombre { get; set; }

    public string? Matricula { get; set; }

    public string? Password { get; set; }
}
