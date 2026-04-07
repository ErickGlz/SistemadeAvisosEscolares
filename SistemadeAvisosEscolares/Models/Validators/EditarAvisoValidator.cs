using FluentValidation;
using SistemadeAvisosEscolares.Models.DTOs;

namespace SistemadeAvisosEscolares.Models.Validators
{
    public class EditarAvisoValidator : AbstractValidator<EditarAvisoDTO>
    {
        public EditarAvisoValidator()
        {
            RuleFor(x => x.IdAviso)
                .GreaterThan(0).WithMessage("El aviso no existe");

            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("El título es obligatorio")
                .MaximumLength(200);

            RuleFor(x => x.Contenido)
                .NotEmpty().WithMessage("El contenido es obligatorio");
        }

    }
}
