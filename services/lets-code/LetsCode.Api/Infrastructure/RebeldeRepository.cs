using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using LetsCode.Api.Domain.AggregateRebelde;

namespace LetsCode.Api.Infrastructure
{
    public class RebeldeRepository : IRebeldeRepository
    {
        public readonly IReadOnlyCollection<Recurso> recursos = new List<Recurso>()
        {
            new Recurso(TipoRecurso.Arma, quantidade: 0, pontos: 4),
            new Recurso(TipoRecurso.Municao, quantidade: 0, pontos: 3),
            new Recurso(TipoRecurso.Agua, quantidade: 0, pontos: 2),
            new Recurso(TipoRecurso.Comida, quantidade: 0, pontos: 1)
        };

        private static ICollection<Rebelde> rebeldes = new Collection<Rebelde>();
        public ICollection<Rebelde> Rebeldes => rebeldes;

        public void Add(Rebelde rebelde)
        {
            rebeldes.Add(rebelde);
        }

        public Rebelde ObterPorId(Guid id) =>
            Rebeldes.FirstOrDefault(_ => _.Id.Equals(id));

        public void Atualizar(Rebelde rebelde)
        {
            rebeldes = rebeldes.Where(_ => !_.Id.Equals(rebelde.Id)).ToList();
            rebeldes.Add(rebelde);
        }

        public IReadOnlyCollection<Recurso> ObterRecursos() => recursos;

        public IReadOnlyCollection<Rebelde> ObterTodos() => Rebeldes.ToImmutableArray();
    }
}
