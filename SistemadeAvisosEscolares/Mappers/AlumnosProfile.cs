using AutoMapper;
using SistemadeAvisosEscolares.Models.DTOs;
using SistemadeAvisosEscolares.Models.Entities;

namespace SistemadeAvisosEscolares.Mappers
{
    public class AlumnosProfile : Profile
    {
        public AlumnosProfile()
        {
            CreateMap<Alumnos, AlumnoDTO>();

            CreateMap<AlumnoLoginDTO, Alumnos>();
        }
    }
}
