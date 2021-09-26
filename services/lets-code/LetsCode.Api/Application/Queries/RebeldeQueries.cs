using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LetsCode.Api.Domain.AggregateRebelde;
using LetsCode.Api.Domain.Services;
using SharedKernel.Extensions;
using static LetsCode.Api.Application.Commands.NegociarRecursosEntreRebeldesCommand.RebeldeRecursosDTO;
using static LetsCode.Api.Application.Queries.RebeldeQueriesDTO;
using static LetsCode.Api.Application.Queries.RebeldeQueriesDTO.RelatorioDTO;

namespace LetsCode.Api.Application.Queries
{
    public sealed class RebeldeQueries : IRebeldeQueries
    {
        private readonly IRebeldeRepository _rebeldeRepository;
        private readonly IRebeldeDomainService _rebeldeDomainService;

        public RebeldeQueries(IRebeldeRepository rebeldeRepository,
            IRebeldeDomainService rebeldeDomainService)
        {
            _rebeldeRepository = rebeldeRepository;
            _rebeldeDomainService = rebeldeDomainService;
        }

        public async Task<RelatorioDTO> GerarRelatoriosAsync()
        {
            await Task.CompletedTask;

            var rebeldes = _rebeldeRepository.ObterTodos();
            var rebeldesTraidores = rebeldes.Where(_ => _.Traidor());
            var rebeldesNaoTraidores = rebeldes.Where(_ => !_.Traidor());

            var totalRebeldes = rebeldesNaoTraidores.Count();
            var totalRebeldesTraidores = rebeldesNaoTraidores.Where(_ => _.Traidor()).Count();
            var totalRebeldesNaoTraidores = rebeldesNaoTraidores.Count() - totalRebeldesTraidores;

            var porcentagemRebeldesTraidores = totalRebeldesTraidores == 0 ? 0
                : totalRebeldes / totalRebeldesTraidores;
            var porcentagemRebeldesNaoTraidores = totalRebeldesNaoTraidores == 0 ? 0
                : totalRebeldes / totalRebeldesNaoTraidores;

            var recursosDTO = ObterRecursosDTO(rebeldesTraidores.SelectMany(_ => _.Inventario.Recursos)).ToArray();
            var pontosPerdidosPorContaDeTraidores =
                _rebeldeDomainService.ObterTotalPontosDosRecursos(recursosDTO);

            var recursosDeTodosRebeldesNaoTraidores = rebeldesNaoTraidores.SelectMany(_ => _.Inventario.Recursos);
            var recursosPorRebelde = recursosDeTodosRebeldesNaoTraidores
                .GroupBy(_ => _.Tipo)
                .Select(_ => new RecursoPorRebeldeDTO
                {
                    DescricaoRecurso = EnumExtension.ObterDescricaoEnum(_.Key),
                    QuantidadeRecurso = totalRebeldes == 0 ? 0 : _.Sum(_ => _.Quantidade) / totalRebeldes
                })
                .ToArray();

            return new RelatorioDTO
            {
                PorcentagemTraidores = porcentagemRebeldesTraidores,
                PorcentagemRebeldes = porcentagemRebeldesNaoTraidores,
                PontosPerdidosPorContaDeTraidores = pontosPerdidosPorContaDeTraidores,
                RecursosPorRebelde = recursosPorRebelde
            };
        }

        public IReadOnlyCollection<Rebelde> ObterTodosRebeldes()
        {
            return _rebeldeRepository.ObterTodos();
        }

        private IEnumerable<NegociacaoRecursosDTO> ObterRecursosDTO(IEnumerable<Recurso> recursos)
        {
            foreach (var recurso in recursos)
            {
                yield return new NegociacaoRecursosDTO
                {
                    RecursoId = recurso.Id,
                    Tipo = recurso.Tipo,
                    Quantidade = recurso.Quantidade
                };
            }
        }
    }
}