using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using LetsCode.Api.Domain.Services;
using MediatR;

namespace LetsCode.Api.Application.Commands
{
    public class RebeldeCommandHandler :
        IRequestHandler<AdicionarRebeldeCommand, ValidationResult>,
        IRequestHandler<AtualizarLocalizacaoRebeldeCommand, ValidationResult>,
        IRequestHandler<ReportarRebeldeComoTraidorCommand, ValidationResult>,
        IRequestHandler<NegociarRecursosEntreRebeldesCommand, ValidationResult>
    {
        private readonly IRebeldeDomainService _rebeldeDomainService;

        public RebeldeCommandHandler(IRebeldeDomainService rebeldeDomainService)
        {
            _rebeldeDomainService = rebeldeDomainService;
        }

        public async Task<ValidationResult> Handle(AdicionarRebeldeCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.Validation();

            await Task.CompletedTask;

            _rebeldeDomainService.AdicionarRebelde(request.Genero, request.Idade, request.Nome, request.Inventario, request.Localizacao);

            return request.Validation();
        }

        public async Task<ValidationResult> Handle(AtualizarLocalizacaoRebeldeCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.Validation();

            _rebeldeDomainService.AtualizarLocalizacao(request.Localizacao, request.RebeldeId);

            await Task.CompletedTask;

            return request.Validation();
        }

        public async Task<ValidationResult> Handle(ReportarRebeldeComoTraidorCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.Validation();

            _rebeldeDomainService.ReportarRebeldeComoTraidor(request.RebeldeId);

            await Task.CompletedTask;

            return request.Validation();
        }

        public async Task<ValidationResult> Handle(NegociarRecursosEntreRebeldesCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.Validation();

            _rebeldeDomainService.NegociarRecursosEntreRebeldes(request.RebeldeDe, request.RebeldePara);

            await Task.CompletedTask;

            return request.Validation();
        }
    }
}