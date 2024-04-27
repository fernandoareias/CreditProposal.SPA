using System;
using Atividade02.Core.Common.Validators.Interfaces;
using Atividade02.Core.Mediator.Interfaces;
using Atividade02.Proposals.API.DTOs.Requests;
using Atividade02.Proposals.Application.Proposals.Commands;
using Atividade02.Proposals.Application.Proposals.Commands.Views;
using Atividade02.Proposals.Application.Proposals.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Atividade02.Proposals.API.Controllers
{
    [Route("proposals")]
    [ApiController]
    public class ProposalController : CommonController
    {
        public ProposalController(
            IMediatorHandler mediatorHandler,
            IValidatorServices validatorServices
        )
        : base(mediatorHandler, validatorServices)
        {
        }




        /// <summary>
        /// Get all proposals from cpf + cnpj
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? cpf, [FromQuery] string? cnpj)
        {
            var response = await _mediatorHandler.Execute(new GetListProposalByFilterQuery(cpf, cnpj));

            return Ok(response);
        }

        /// <summary>
        /// Get detail proposal
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("{aggregateId}")]
        public async Task<IActionResult> GetById(string aggregateId)
        {
            var response = await _mediatorHandler.Execute(new GetProposalByAggregateIdQuery(aggregateId));

            if (response is null) return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Create credit proposal
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Send([FromHeader(Name = "correlation-id")] Guid correlationid, [FromBody] CreateProposalRequest request)
        {
            var response = await _mediatorHandler.Send<SendProposalCommand>(new SendProposalCommand(
                correlationid,
                request.Name,
                request.CPF,
                request.CNPJ,
                request.DDD,
                request.Cellphone
            ));

            if (response is null && HasError)
                return ReturnBadRequestWithErrors<SendProposalCommandView>();

            return Accepted(response);
        }


    }
}

