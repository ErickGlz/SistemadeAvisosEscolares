using System;
using System.Collections.Generic;

namespace SistemadeAvisosEscolares.Models.Entities;

public partial class Maestros
{
    public int IdMaestro { get; set; }

    public string? Nombre { get; set; }

    public string? Password { get; set; }

    public string? Grupo { get; set; }
}
