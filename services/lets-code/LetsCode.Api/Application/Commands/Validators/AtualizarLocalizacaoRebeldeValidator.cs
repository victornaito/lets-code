using System;
using FluentValidation;

namespace LetsCode.Api.Application.Commands.Validators
{
    public sealed class AtualizarLocalizacaoRebeldeValidator : AbstractValidator<AtualizarLocalizacaoRebeldeCommand>
    {
        public AtualizarLocalizacaoRebeldeValidator()
        {
            RuleFor(_ => _.RebeldeId)
                .NotEmpty()
                .WithMessage("Informe Rebelde Id");

            RuleFor(_ => _.Localizacao)
                .NotNull()
                .WithMessage("Informe a Localizacao");
        }
    }
}
