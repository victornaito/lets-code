using System;
using System.Collections.Generic;
using FluentValidation.Results;
using LetsCode.Api.Application.Commands.Validators;
using MediatR;
using Shared.Api;
using static LetsCode.Api.Application.Commands.AdicionarRebeldeCommand;

namespace LetsCode.Api.Application.Commands
{
    public sealed class AtualizarLocalizacaoRebeldeCommand : CommandBase, IRequest<ValidationResult>
    {
        public LocalizacaoDTO Localizacao { get; set; }
        public Guid RebeldeId { get; set; }

        public override bool IsValid()
        {
            SetValidation(new AtualizarLocalizacaoRebeldeValidator().Validate(this));
            return Validation().IsValid;
        }
    }
}