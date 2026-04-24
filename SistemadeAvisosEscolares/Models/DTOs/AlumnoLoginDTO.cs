namespace SistemadeAvisosEscolares.Models.DTOs
{
    public class AlumnoDTO
    {
        public int IdAlumno { get; set; }
        public string Nombre { get; set; } = null!;
        public string Matricula { get; set; } = null!;
        public string Grupo { get; set; } = null!;
    }

    public class AlumnoLoginDTO
    {
        public string Matricula { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
