using AutoMapper;
using SistemadeAvisosEscolares.Models.DTOs;
using SistemadeAvisosEscolaresApi.Models.Entities;

namespace SistemadeAvisosEscolares.Mappers
{
    public class MaestrosProfile : Profile
    {
        public MaestrosProfile()
        {
            CreateMap<Maestros, MaestroDTO>();

            CreateMap<MaestroLoginDTO, Maestros>();
        }
    }
}
