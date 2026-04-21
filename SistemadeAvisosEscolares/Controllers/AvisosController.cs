using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemadeAvisosEscolares.Models.DTOs;
using SistemadeAvisosEscolares.Services;

namespace SistemadeAvisosEscolares.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvisosController : ControllerBase
    {
        private readonly AvisosService service;

        public AvisosController(AvisosService service)
        {
            this.service = service;
        }

        [HttpGet("alumno/{idAlumno}")]
        public IActionResult GetAvisosAlumno(int idAlumno)
        {
            var avisos = service.GetAvisosAlumno(idAlumno);

            if (avisos == null || !avisos.Any())
            {
                return NotFound();
            }

            return Ok(avisos);
        }

        [HttpPost]
        public IActionResult CrearAviso(CrearAvisoDTO dto)
        {
            try
            {
                service.CrearAviso(dto);
                return Ok();
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

        [HttpPut]
        public IActionResult EditarAviso(EditarAvisoDTO dto)
        {
            try
            {
                service.EditarAviso(dto);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(x => x.ErrorMessage));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("leido")]
        public IActionResult MarcarLeido(AvisoLeidoDTO dto)
        {
            try
            {
                service.MarcarLeido(dto);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(x => x.ErrorMessage));
            }
        }
        [HttpDelete("{idAviso}")]
        public IActionResult EliminarAviso(int idAviso)
        {
            try
            {
                service.EliminarAviso(idAviso);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpGet("expirados/{idAlumno}")]
        public IActionResult GetExpirados(int idAlumno)
        {
            var avisos = service.GetAvisosExpirados(idAlumno);

            if (avisos == null || !avisos.Any())
            {
                return NotFound();
            }

            return Ok(avisos);
        }
    }
}
