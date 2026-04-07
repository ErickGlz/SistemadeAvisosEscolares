namespace SistemadeAvisosEscolares.Models.DTOs
{
    public class AvisoDTO
    {
        public int IdAviso { get; set; }
        public string Titulo { get; set; } = null!;
        public string Contenido { get; set; } = null!;
        public string Maestro { get; set; } = null!;
        public int IdMaestro { get; set; }

        public int? IdAlumno { get; set; }  
        public string TipoAviso { get; set; } = null!;
        public DateTime FechaEnvio { get; set; }
        public DateTime? FechaLeido { get; set; }
    }

    public class CrearAvisoDTO
    {
        public string Titulo { get; set; } = null!;
        public string Contenido { get; set; } = null!;
        public int IdMaestro { get; set; }
        public int? IdAlumno { get; set; }   
        public string TipoAviso { get; set; } = null!;
        public DateTime FechaCaducidad { get; set; }
    }

        public class EditarAvisoDTO
    {
        public int IdAviso { get; set; }
        public string Titulo { get; set; } = null!;
        public string Contenido { get; set; } = null!;
        public DateTime FechaCaducidad { get; set; }
    }

    public class AvisoLeidoDTO
    {
        public int Id { get; set; }
        public int IdAviso { get; set; }
        public int IdAlumno { get; set; }

        public DateTime? FechaLeido { get; set; }
    }
}
