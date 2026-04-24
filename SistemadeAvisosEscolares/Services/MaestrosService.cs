using AutoMapper;
using SistemadeAvisosEscolares.Models.DTOs;
using SistemadeAvisosEscolares.Repositories;
using SistemadeAvisosEscolaresApi.Models.Entities;

namespace SistemadeAvisosEscolares.Services
{
    public class MaestrosService
    {
        private readonly Repository<Maestros> repository;
        private readonly Repository<Avisos> avisosRepository;
        private readonly Repository<Alumnos> repositoryAlumnos;
        private readonly IMapper mapper;

        public MaestrosService(
            Repository<Maestros> repository,
            Repository<Avisos> avisosRepository,
            Repository<Alumnos> repositoryAlumnos,
            IMapper mapper)
        {
            this.repository = repository;
            this.avisosRepository = avisosRepository;
            this.repositoryAlumnos = repositoryAlumnos;
            this.mapper = mapper;
        }

        public List<AvisoDTO> VerAvisosMaestro(int idMaestro)
        {
            var avisos = avisosRepository.Query()
                .Where(x => x.IdMaestro == idMaestro);

            return avisos
                .Select(x => mapper.Map<AvisoDTO>(x))
                .ToList();
        }
        public MaestroDTO? Login(MaestroLoginDTO dto)
        {
            var maestro = repository.Query()
                .FirstOrDefault(x =>
                    x.IdMaestro == dto.IdMaestro &&
                    x.Password == dto.Password);

            if (maestro == null)
            {
                return null;
            }

            return mapper.Map<MaestroDTO>(maestro);
        }

        public List<AlumnoDTO> GetAlumnos(int idMaestro)
        {
            return repositoryAlumnos.Query()
                .Where(a => a.IdMaestro == idMaestro)
                .Select(a => mapper.Map<AlumnoDTO>(a))
                .ToList();
        }
    }
}
