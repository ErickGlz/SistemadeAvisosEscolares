using System;
using System.Collections.Generic;
using System.Text;

namespace AplicacionAvisosEscolares.Models
{
    public class AlumnoDTO
    {
        public int IdAlumno { get; set; }
        public string Nombre { get; set; } = null!;
        public string Matricula { get; set; } = null!;
        public string Grupo { get; set; } = null!;
    }
}
