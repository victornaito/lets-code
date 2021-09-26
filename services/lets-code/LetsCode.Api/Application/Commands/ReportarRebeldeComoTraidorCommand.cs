using System;
using FluentValidation.Results;
using LetsCode.Api.Application.Commands.Validators;
using MediatR;
using Shared.Api;

namespace LetsCode.Api.Application.Commands
{
    public sealed class ReportarRebeldeComoTraidorCommand : CommandBase, IRequest<ValidationResult>
    {
        public Guid RebeldeId { get; set; }

        public override bool IsValid()
        {
            SetValidation(new ReportarRebeldeComoTraidorValidator().Validate(this));
            return Validation().IsValid;
        }
    }
}