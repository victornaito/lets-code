using System;
using Shared.Domain;

namespace LetsCode.Api.Domain.AggregateRebelde
{
    public class Recurso : EntityGuid
    {
        public Recurso(TipoRecurso tipo, uint quantidade = 0, uint pontos = 0)
        {
            Tipo = tipo;
            Quantidade = quantidade;
            Pontos = pontos;
        }

        public Recurso(Guid id, TipoRecurso tipo, uint quantidade = 0, uint pontos = 0)
        {
            Id = id;
            Tipo = tipo;
            Quantidade = quantidade;
            Pontos = pontos;
        }

        public TipoRecurso Tipo { get; private set; }
        public uint Quantidade { get; private set; }
        public uint Pontos { get; private set; }

        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}