using System;
using System.Collections.Generic;
using LetsCode.Api.Application.Commands;
using LetsCode.Api.Domain.AggregateRebelde;
using static LetsCode.Api.Application.Commands.AdicionarRebeldeCommand;
using static LetsCode.Api.Application.Commands.NegociarRecursosEntreRebeldesCommand;
using static LetsCode.Api.Application.Commands.NegociarRecursosEntreRebeldesCommand.RebeldeRecursosDTO;

namespace LetsCode.Api.Domain.Services
{
    public interface IRebeldeDomainService
    {
        void AdicionarRebelde(string genero, uint idade, string nome, InventarioDTO inventario, LocalizacaoDTO localizacao);
        void AtualizarLocalizacao(LocalizacaoDTO localizacao, System.Guid rebeldeId);
        void NegociarRecursosEntreRebeldes(RebeldeRecursosDTO rebeldeDe, RebeldeRecursosDTO rebeldePara);
        uint ObterTotalPontosDosRecursos(ICollection<NegociacaoRecursosDTO> recursos);
        void ReportarRebeldeComoTraidor(Guid rebeldeId);
    }
}