using AutoMapper;
using SistemadeAvisosEscolares.Models.DTOs;
using SistemadeAvisosEscolares.Models.Entities;
using SistemadeAvisosEscolares.Repositories;

namespace SistemadeAvisosEscolares.Services
{
    public class AlumnosService
    {
        private readonly Repository<Alumnos> repository;
        private readonly IMapper mapper;

        public AlumnosService(
            Repository<Alumnos> repository,
            Repository<Avisos> avisosRepository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

      
        public AlumnoDTO? Login(AlumnoLoginDTO dto)
        {
            var alumno = repository.Query()
                .FirstOrDefault(x =>
                    x.Matricula == dto.Matricula &&
                    x.Password == dto.Password);

            if (alumno == null)
            {
                return null;
            }

            return mapper.Map<AlumnoDTO>(alumno);
        }

        public AlumnoDTO? GetByMatricula(string matricula)
        {
            var alumno = repository.Query()
                .FirstOrDefault(x => x.Matricula == matricula);

            if (alumno == null)
                return null;

            return mapper.Map<AlumnoDTO>(alumno);
        }
    }
}
