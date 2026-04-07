using FluentValidation;
using SistemadeAvisosEscolares.Models.DTOs;

namespace SistemadeAvisosEscolares.Models.Validators
{
    public class AlumnoLoginValidator : AbstractValidator<AlumnoLoginDTO>
    {
        public AlumnoLoginValidator()
        {
            RuleFor(x => x.Matricula)
                .NotEmpty().WithMessage("Ingrese la matrícula");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Ingrese la contraseña");
        }
    
    }
}
