using System;
using FluentValidation;

namespace LetsCode.Api.Application.Commands.Validators
{
    public sealed class ReportarRebeldeComoTraidorValidator : AbstractValidator<ReportarRebeldeComoTraidorCommand>
    {
        public ReportarRebeldeComoTraidorValidator()
        {
            RuleFor(_ => _.RebeldeId)
                .NotEmpty()
                .WithMessage("Informe o Rebelde Id");
        }
    }
}
