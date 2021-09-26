using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LetsCode.Api.Domain.AggregateRebelde;
using static LetsCode.Api.Application.Queries.RebeldeQueriesDTO;

namespace LetsCode.Api.Application.Queries
{
    public interface IRebeldeQueries
    {
        Task<RelatorioDTO> GerarRelatoriosAsync();
        IReadOnlyCollection<Rebelde> ObterTodosRebeldes();
    }
}