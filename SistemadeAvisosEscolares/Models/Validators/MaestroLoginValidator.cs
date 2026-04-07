using FluentValidation;
using SistemadeAvisosEscolares.Models.DTOs;

namespace SistemadeAvisosEscolares.Models.Validators
{
    public class MaestroLoginValidator : AbstractValidator<MaestroLoginDTO>
    {
        public MaestroLoginValidator()
        {
            RuleFor(x => x.IdMaestro)
                .GreaterThan(0).WithMessage("Ingrese el ID del maestro");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Ingrese la contraseña");
        }
    
    }
}
