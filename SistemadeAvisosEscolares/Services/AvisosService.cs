using AutoMapper;
using FluentValidation;
using SistemadeAvisosEscolares.Models.DTOs;
using SistemadeAvisosEscolares.Models.Entities;
using SistemadeAvisosEscolares.Models.Validators;
using SistemadeAvisosEscolares.Repositories;

namespace SistemadeAvisosEscolares.Services
{
    public class AvisosService
    {
        private readonly Repository<Avisos> repository;
        private readonly Repository<Avisosalumnos> repositoryLeidos;
        private readonly Repository<Maestros> repositoryMaestros;
        private readonly IMapper mapper;
        private readonly CrearAvisoValidator crearValidator;

        public AvisosService(
            Repository<Avisos> repository,
            Repository<Avisosalumnos> repositoryLeidos,
            Repository<Maestros> repositoryMaestros,
            IMapper mapper,
            CrearAvisoValidator crearValidator)
        {
            this.repository = repository;
            this.repositoryLeidos = repositoryLeidos;
            this.repositoryMaestros = repositoryMaestros;
            this.mapper = mapper;
            this.crearValidator = crearValidator;
        }

        public List<AvisoDTO> GetAvisosAlumno(int idAlumno)
        {
            var avisos = repository.Query()
                .Where(x =>
                    (x.TipoAviso == "GENERAL" && x.FechaCaducidad >= DateTime.Now) ||
                    (x.TipoAviso == "PERSONAL" && x.IdAlumno == idAlumno))
                .ToList();

            var lista = new List<AvisoDTO>();

            foreach (var aviso in avisos)
            {
                var dto = mapper.Map<AvisoDTO>(aviso);

                dto.Maestro = repositoryMaestros.Query()
                    .Where(x => x.IdMaestro == aviso.IdMaestro)
                    .Select(x => x.Nombre)
                    .FirstOrDefault() ?? "";

                dto.FechaLeido = repositoryLeidos.Query()
                    .Where(x => x.IdAviso == aviso.IdAviso && x.IdAlumno == idAlumno)
                    .Select(x => x.FechaLeido)
                    .FirstOrDefault();

                lista.Add(dto);
            }

            return lista;
        }

        public void CrearAviso(CrearAvisoDTO dto)
        {
            var result = crearValidator.Validate(dto);

            if (result.IsValid)
            {
                var entidad = mapper.Map<Avisos>(dto);
                entidad.FechaEnvio = DateTime.Now;

                repository.Insert(entidad);
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public void EditarAviso(EditarAvisoDTO dto)
        {
            var aviso = repository.Get(dto.IdAviso);

            if (aviso == null)
            {
                throw new KeyNotFoundException();
            }

            mapper.Map(dto, aviso);
            repository.Update(aviso);
        }

        public void MarcarLeido(AvisoLeidoDTO dto)
        {
            var existe = repositoryLeidos.Query()
                .FirstOrDefault(x => x.IdAviso == dto.IdAviso && x.IdAlumno == dto.IdAlumno);

            if (existe == null)
            {
                var entidad = new Avisosalumnos
                {
                    IdAviso = dto.IdAviso,
                    IdAlumno = dto.IdAlumno,
                    FechaLeido = DateTime.Now
                };

                repositoryLeidos.Insert(entidad);
            }
        }
        public void EliminarAviso(int idAviso)
        {
            var aviso = repository.Get(idAviso);

            if (aviso == null)
            {
                throw new KeyNotFoundException();
            }

            repository.Delete(idAviso);
        }
        public List<AvisoDTO> GetAvisosExpirados(int idAlumno)
        {
            var avisos = repository.Query()
                .Where(x =>
                    (x.TipoAviso == "GENERAL" && x.FechaCaducidad < DateTime.Now) ||
                    (x.TipoAviso == "PERSONAL" && x.IdAlumno == idAlumno && x.FechaCaducidad < DateTime.Now))
                .ToList();

            var lista = new List<AvisoDTO>();

            foreach (var aviso in avisos)
            {
                var dto = mapper.Map<AvisoDTO>(aviso);

                dto.Maestro = repositoryMaestros.Query()
                    .Where(x => x.IdMaestro == aviso.IdMaestro)
                    .Select(x => x.Nombre)
                    .FirstOrDefault() ?? "";

                dto.FechaLeido = repositoryLeidos.Query()
                    .Where(x => x.IdAviso == aviso.IdAviso && x.IdAlumno == idAlumno)
                    .Select(x => x.FechaLeido)
                    .FirstOrDefault();

                lista.Add(dto);
            }

            return lista;
        }
    }

}
