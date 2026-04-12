using System;
using System.Collections.Generic;
using System.Text;

namespace AplicacionAvisosEscolares.Models
{
    public class MaestroDTO
    {
        public int IdMaestro { get; set; }
        public string Nombre { get; set; } = null!;
        public string Grupo { get; set; } = null!;
    }
}
