using System;
using System.Collections.Generic;
using FluentValidation.Results;
using LetsCode.Api.Application.Commands.Validators;
using LetsCode.Api.Domain.AggregateRebelde;
using MediatR;
using Shared.Api;

namespace LetsCode.Api.Application.Commands
{
    public sealed class NegociarRecursosEntreRebeldesCommand : CommandBase, IRequest<ValidationResult>
    {
        public RebeldeRecursosDTO RebeldeDe { get; set; }
        public RebeldeRecursosDTO RebeldePara { get; set; }

        public override bool IsValid()
        {
            SetValidation(new NegociarRecursosEntreRebeldesValidator().Validate(this));
            return Validation().IsValid;
        }

        public class RebeldeRecursosDTO
        {
            public Guid RebeldeId { get; set; }
            public ICollection<NegociacaoRecursosDTO> Recursos { get; set; }

            public class NegociacaoRecursosDTO
            {
                public Guid RecursoId { get; set; }
                public TipoRecurso Tipo { get; set; }
                public uint Quantidade { get; set; }   
            }
        }
    }
}