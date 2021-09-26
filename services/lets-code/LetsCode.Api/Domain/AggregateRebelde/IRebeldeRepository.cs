using System;
using System.Collections.Generic;

namespace LetsCode.Api.Domain.AggregateRebelde
{
    public interface IRebeldeRepository
    {
        void Add(Rebelde rebelde);
        Rebelde ObterPorId(Guid id);
        void Atualizar(Rebelde rebelde);
        IReadOnlyCollection<Rebelde> ObterTodos();
        IReadOnlyCollection<Recurso> ObterRecursos();
    }
}
