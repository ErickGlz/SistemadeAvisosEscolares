using FluentValidation;
using SistemadeAvisosEscolares.Models.DTOs;

namespace SistemadeAvisosEscolares.Models.Validators
{
    public class AvisoLeidoValidator : AbstractValidator<AvisoLeidoDTO>
    {
        public AvisoLeidoValidator()
        {
            RuleFor(x => x.IdAviso)
                .GreaterThan(0).WithMessage("Aviso inválido");

            RuleFor(x => x.IdAlumno)
                .GreaterThan(0).WithMessage("Alumno inválido");
        }
    
    }
}
