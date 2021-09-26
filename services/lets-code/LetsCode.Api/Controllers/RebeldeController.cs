using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using LetsCode.Api.Application.Commands;
using LetsCode.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace LetsCode.Api.Controllers
{

    public class RebeldeController : CustomController
    {
        private readonly ILogger<RebeldeController> _logger;
        private readonly IRebeldeQueries _rebeldeQueries;
        private readonly IMediator _mediator;

        public RebeldeController(ILogger<RebeldeController> logger,
                                 IRebeldeQueries rebeldeQueries,
                                 IMediator mediator)
        {
            _logger = logger;
            _rebeldeQueries = rebeldeQueries;
            _mediator = mediator;
        }


        [HttpGet("todos")]
        [SwaggerOperation(
            Summary = "Gera os Relatatórios vinculados aos rebeldes",
            Description = "Gera os relatórios sobre os rebeldes, traidores e seus recursos"
        )]
        public IActionResult ObterTodosOsRebeldes() =>
             Ok(_rebeldeQueries.ObterTodosRebeldes());

        [HttpGet]
        [SwaggerOperation(
            Summary = "Gera os Relatatórios vinculados aos rebeldes",
            Description = "Gera os relatórios sobre os rebeldes, traidores e seus recursos"
        )]
        public async Task<IActionResult> ObterRelatoriosAsync() =>
             Ok(await _rebeldeQueries.GerarRelatoriosAsync());

        [HttpPost]
        [SwaggerOperation(
            Summary = "Gera os Relatatórios vinculados aos rebeldes",
            Description = "Gera os relatórios sobre os rebeldes, traidores e seus recursos"
        )]
        public async Task<IActionResult> AdicionarRebeldeAsync(AdicionarRebeldeCommand command) =>
            CustomReponse(await _mediator.Send(command));

        [HttpPut("{rebeldeId}/localizacao")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Atualiza a localização do rebelde"
        )]
        public async Task<IActionResult> AtualizarLocalizacaoRebeldeCommand(Guid rebeldeId, [FromBody] AtualizarLocalizacaoRebeldeCommand command)
        {
            command.RebeldeId = rebeldeId;
            return CustomReponse(await _mediator.Send(command));
        }

        [HttpPut("negociar-recursos")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Negocia recursos entre rebeldes",
            Description = "Negocia recursos entre rebeldes, sendo os mesmos não traidores"
        )]
        public async Task<IActionResult> NegociarRecursosEntreRebeldesAsync([FromBody] NegociarRecursosEntreRebeldesCommand command)
        {
            return CustomReponse(await _mediator.Send(command));
        }

        [HttpPut("reportar-traidor")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Reporta rebelde como traidor",
            Description = "Informa que o rebelde é um traidor"
        )]
        public async Task<IActionResult> ReportarRebeldeComoTraidorAsync([FromBody] ReportarRebeldeComoTraidorCommand command) =>
            CustomReponse(await _mediator.Send(command));
    }
}
