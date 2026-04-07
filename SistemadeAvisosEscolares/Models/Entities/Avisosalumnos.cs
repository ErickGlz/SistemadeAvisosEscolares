using System;
using System.Collections.Generic;

namespace SistemadeAvisosEscolares.Models.Entities;

public partial class Avisosalumnos
{
    public int Id { get; set; }

    public int? IdAviso { get; set; }

    public int? IdAlumno { get; set; }

    public DateTime? FechaRecibido { get; set; }

    public DateTime? FechaLeido { get; set; }
}
