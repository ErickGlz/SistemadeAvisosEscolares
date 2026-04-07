using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemadeAvisosEscolares.Models.DTOs;
using SistemadeAvisosEscolares.Services;

namespace SistemadeAvisosEscolares.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaestrosController : ControllerBase
    {
        private readonly MaestrosService service;

        public MaestrosController(MaestrosService service)
        {
            this.service = service;
        }

        [HttpPost("login")]
        public IActionResult Login(MaestroLoginDTO dto)
        {
            try
            {
                var maestro = service.Login(dto);

                if (maestro == null)
                {
                    return NotFound();
                }

                return Ok(maestro);
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

        [HttpGet("{idMaestro}/avisos")]
        public IActionResult GetAvisos(int idMaestro)
        {
            var avisos = service.VerAvisosMaestro(idMaestro);

            if (avisos == null || !avisos.Any())
            {
                return NotFound();
            }

            return Ok(avisos);
        }
    }
}
