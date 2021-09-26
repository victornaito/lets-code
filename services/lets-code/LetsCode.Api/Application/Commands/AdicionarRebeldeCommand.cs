using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using LetsCode.Api.Application.Commands.Validators;
using LetsCode.Api.Domain.AggregateRebelde;
using MediatR;
using Shared.Api;

namespace LetsCode.Api.Application.Commands
{
    public class AdicionarRebeldeCommand : CommandBase, IRequest<ValidationResult>
    {
        public string Nome { get; set; }
        public uint Idade { get; set; }
        public string Genero { get; set; }
        public LocalizacaoDTO Localizacao { get; set; }
        public InventarioDTO Inventario { get; set; }

        public override bool IsValid()
        {
            SetValidation(new AdicionarRebeldeValidator().Validate(this));
            return Validation().IsValid;
        }

        public class LocalizacaoDTO
        {
            public string Nome { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
        }

        public class InventarioDTO
        {
            public ICollection<RecursoDTO> Recursos { get; set; }

            public class RecursoDTO
            {
                public TipoRecurso Tipo { get; set; }
                public uint Quantidade { get; set; }
            }

        }
    }
}