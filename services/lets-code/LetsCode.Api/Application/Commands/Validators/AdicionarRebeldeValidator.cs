using System;
using FluentValidation;

namespace LetsCode.Api.Application.Commands.Validators
{
    public sealed class AdicionarRebeldeValidator : AbstractValidator<AdicionarRebeldeCommand>
    {
        public AdicionarRebeldeValidator()
        {
            RuleFor(_ => _.Genero)
                .NotEmpty()
                .WithMessage("Informe o Genero");

            RuleFor(_ => _.Idade)
                .NotNull()
                .WithMessage("Informe a idade");

            RuleFor(_ => _.Nome)
                .NotEmpty()
                .WithMessage("Informe o Nome");

            RuleFor(_ => _.Inventario)
                .NotEmpty()
                .WithMessage("Informe ao menos um item na lista de inventário");

            RuleFor(_ => _.Inventario.Recursos)
               .NotEmpty()
               .WithMessage("Informe ao menos um item na lista de Recursos");


            RuleFor(_ => _.Localizacao)
                .NotEmpty()
                .WithMessage("Informe a Localização");
        }
    }
}
