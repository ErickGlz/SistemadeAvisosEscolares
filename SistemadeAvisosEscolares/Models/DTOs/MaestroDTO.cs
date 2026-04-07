namespace SistemadeAvisosEscolares.Models.DTOs
{
        public class MaestroDTO
        {
            public int IdMaestro { get; set; }
            public string Nombre { get; set; } = null!;
            public string Grupo { get; set; } = null!;
        }

    public class MaestroLoginDTO
    {
        public int IdMaestro { get; set; }
        public string Password { get; set; } = null!;
    }
}
