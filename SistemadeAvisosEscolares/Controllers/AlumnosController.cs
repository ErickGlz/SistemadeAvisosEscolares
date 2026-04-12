using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemadeAvisosEscolares.Models.DTOs;
using SistemadeAvisosEscolares.Models.Entities;
using SistemadeAvisosEscolares.Repositories;
using SistemadeAvisosEscolares.Services;

namespace SistemadeAvisosEscolares.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        private readonly AlumnosService service;

        public AlumnosController(AlumnosService service)
        {
            this.service = service;
        }

        [HttpPost("login")]
        public IActionResult Login(AlumnoLoginDTO dto)
        {
            try
            {
                var alumno = service.Login(dto);

                if (alumno == null)
                {
                    return NotFound();
                }

                return Ok(alumno);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(x => x.ErrorMessage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("matricula/{matricula}")]
        public IActionResult GetByMatricula(string matricula)
        {
            var alumno = service.GetByMatricula(matricula);

            if (alumno == null)
                return NotFound();

            return Ok(alumno);
        }
    }
}
