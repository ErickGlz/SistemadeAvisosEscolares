using FluentValidation;
using SistemadeAvisosEscolares.Models.DTOs;

namespace SistemadeAvisosEscolares.Models.Validators
{
    public class CrearAvisoValidator : AbstractValidator<CrearAvisoDTO>
    {
        public CrearAvisoValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("El título es obligatorio")
                .MaximumLength(200).WithMessage("Máximo 200 caracteres");

            RuleFor(x => x.Contenido)
                .NotEmpty().WithMessage("El contenido es obligatorio");

            RuleFor(x => x.TipoAviso)
                .NotEmpty().WithMessage("Debe indicar el tipo de aviso")
                .Must(x => x == "GENERAL" || x == "PERSONAL")
                .WithMessage("El tipo de aviso debe ser GENERAL o PERSONAL");

            RuleFor(x => x.IdMaestro)
                .GreaterThan(0).WithMessage("Debe indicar el maestro que envía el aviso");

            RuleFor(x => x.FechaCaducidad)
                .GreaterThan(DateTime.Now).WithMessage("La fecha de caducidad debe ser futura");

            RuleFor(x => x.IdAlumno)
                .NotNull()
                .When(x => x.TipoAviso == "PERSONAL")
                .WithMessage("Debe indicar el alumno para avisos personales");
        }
    }
}
