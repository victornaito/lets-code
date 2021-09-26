using System;
using System.Collections.Generic;
using System.Linq;
using LetsCode.Api.Domain.AggregateRebelde;
using static LetsCode.Api.Application.Commands.AdicionarRebeldeCommand;
using static LetsCode.Api.Application.Commands.NegociarRecursosEntreRebeldesCommand;
using static LetsCode.Api.Application.Commands.NegociarRecursosEntreRebeldesCommand.RebeldeRecursosDTO;

namespace LetsCode.Api.Domain.Services
{
    public class RebeldeDomainService : IRebeldeDomainService
    {
        private readonly IRebeldeRepository _rebeldeRepository;

        public RebeldeDomainService(IRebeldeRepository rebeldeRepository)
        {
            _rebeldeRepository = rebeldeRepository;
        }

        public void AdicionarRebelde(string genero,
                                     uint idade,
                                     string nome,
                                     InventarioDTO inventario,
                                     LocalizacaoDTO localizacao)
        {
            var rebelde = new RebeldeBuilder()
                .ComId(Guid.NewGuid())
                .ComGenero(genero)
                .ComIdade(idade)
                .ComNome(nome)
                .ComInventario(inventario)
                .ComLocalizacao(localizacao)
                .Build();

            _rebeldeRepository.Add(rebelde);
        }

        public void AtualizarLocalizacao(LocalizacaoDTO localizacaoDTO, Guid rebeldeId)
        {
            Rebelde rebelde = ObterRebeldePorId(rebeldeId);

            var localizacao = new RebeldeBuilder().ComLocalizacao(localizacaoDTO).Build().Localizacao;
            rebelde.AlterarLocalizacao(localizacao);

            _rebeldeRepository.Atualizar(rebelde);
        }

        public void NegociarRecursosEntreRebeldes(RebeldeRecursosDTO rebeldeDeDTO, RebeldeRecursosDTO rebeldeParaDTO)
        {
            var rebeldeDe = ObterRebeldePorId(rebeldeDeDTO.RebeldeId);
            var rebeldePara = ObterRebeldePorId(rebeldeParaDTO.RebeldeId);

            if (rebeldeDe.Traidor() || rebeldePara.Traidor())
                    throw new Exception("Não é possível negociação com rebelde traidor");

            if (!RebeldePossuiRecursoDisponivelParaNegociacao(rebeldeDeDTO, rebeldeDe) ||
                !RebeldePossuiRecursoDisponivelParaNegociacao(rebeldeParaDTO, rebeldePara))
                    throw new Exception("Não é possível negociação pois rebelde não possui recurso disponível");

            var pontosDosRecursosRebeldeDe = ObterTotalPontosDosRecursos(rebeldeDeDTO.Recursos.ToArray());
            var pontosDosRecursosRebeldePara = ObterTotalPontosDosRecursos(rebeldeParaDTO.Recursos.ToArray());

            if (pontosDosRecursosRebeldeDe != pontosDosRecursosRebeldePara)
                throw new Exception("Negociação inválida! A pontuação deve ser a mesma");

            if (ExisteRecursoNovo(rebeldeDeDTO.Recursos, rebeldeParaDTO.Recursos))
                throw new Exception("Negociação inválida! Negociando recurso inexistente");

            rebeldeDe.RemoverRecursos(rebeldeDeDTO.Recursos.Select(_ => _.RecursoId).ToArray());
            rebeldeDe.AlterarRecursos(ObterRecursos(rebeldeParaDTO.Recursos).ToList());

            rebeldePara.RemoverRecursos(rebeldeParaDTO.Recursos.Select(_ => _.RecursoId).ToArray());
            rebeldePara.AlterarRecursos(ObterRecursos(rebeldeDeDTO.Recursos).ToList());
        }

        private bool RebeldePossuiRecursoDisponivelParaNegociacao(RebeldeRecursosDTO rebeldeDeDTO, Rebelde rebelde) =>
            rebelde.Inventario.Recursos.Sum(_ => _.Quantidade) > rebeldeDeDTO.Recursos.Sum(_ => _.Quantidade);

        public uint ObterTotalPontosDosRecursos(ICollection<NegociacaoRecursosDTO> recursos)
        {
            uint totalPontos = 0;

            foreach (var recurso in recursos)
            {
                switch(recurso.Tipo)
                {
                    case TipoRecurso.Arma:
                        totalPontos += 4 * recurso.Quantidade;
                        break;
                    case TipoRecurso.Municao:
                        totalPontos += 3 * recurso.Quantidade;
                        break;
                    case TipoRecurso.Agua:
                        totalPontos += 2 * recurso.Quantidade;
                        break;
                    case TipoRecurso.Comida:
                        totalPontos += 1 * recurso.Quantidade;
                        break;
                    default:
                        throw new ArgumentException("Tipo De Recurso Inválido");
                }
            }

            return totalPontos;
        }

        private bool ExisteRecursoNovo(ICollection<NegociacaoRecursosDTO> recursosDe, ICollection<NegociacaoRecursosDTO> recursosPara)
        => recursosDe.Any(_ => _.RecursoId.Equals(Guid.Empty)) ||
           recursosPara.Any(_ => _.RecursoId.Equals(Guid.Empty));

        private IEnumerable<Recurso> ObterRecursos(IEnumerable<NegociacaoRecursosDTO> recursosDTO)
        {
            foreach(var recurso in recursosDTO)
                yield return new Recurso(recurso.Tipo, recurso.Quantidade);
        }

        private Rebelde ObterRebeldePorId(Guid rebeldeId)
        {
            var rebelde = _rebeldeRepository.ObterPorId(rebeldeId);
            if (rebelde is null) throw new Exception($"Rebelde nao econtrado: {rebeldeId}");
            return rebelde;
        }

        public void ReportarRebeldeComoTraidor(Guid rebeldeId)
        {
            var rebelde = ObterRebeldePorId(rebeldeId);
            rebelde.AdicionarTraicao();
        }
    }
}
