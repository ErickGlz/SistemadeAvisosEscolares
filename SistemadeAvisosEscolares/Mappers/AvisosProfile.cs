using AutoMapper;
using SistemadeAvisosEscolares.Models.DTOs;
using SistemadeAvisosEscolares.Models.Entities;

namespace SistemadeAvisosEscolares.Mappers
{
    public class AvisosProfile : Profile
    {
        public AvisosProfile()
        {
            CreateMap<Avisos, AvisoDTO>();

            CreateMap<CrearAvisoDTO, Avisos>()
                .ForMember(x => x.FechaEnvio,
                    opt => opt.MapFrom(x => DateTime.Now));

            CreateMap<EditarAvisoDTO, Avisos>();
        }
    }
}
