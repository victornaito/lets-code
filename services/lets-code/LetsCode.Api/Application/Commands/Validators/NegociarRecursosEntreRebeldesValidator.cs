using System;
using FluentValidation;

namespace LetsCode.Api.Application.Commands.Validators
{
    public sealed class NegociarRecursosEntreRebeldesValidator : AbstractValidator<NegociarRecursosEntreRebeldesCommand>
    {
        public NegociarRecursosEntreRebeldesValidator()
        {
            RuleFor(_ => _.RebeldeDe)
                .NotEmpty()
                .WithMessage("Informe o Rebelde De");

            RuleFor(_ => _.RebeldePara)
                .NotEmpty()
                .WithMessage("Informe o Rebelde Para");
        }
    }
}
