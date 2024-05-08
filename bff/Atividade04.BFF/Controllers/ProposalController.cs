using System;
using Atividade02.BFF.Models.Events;
using Atividade02.Core.Common.Validators.Interfaces;
using Atividade02.Core.Mediator.Interfaces;
using Atividade02.Core.MessageBus.Services.Interfaces;
using Atividade04.BFF.DTOs.Proposals.v1.Requests;
using Atividade04.BFF.Models.Interfaces;
using Atividade04.BFF.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Atividade04.BFF.Controllers
{

    [ApiController]
    [Route("proposals")]
    public class ProposalController : CommonController
    {

        private readonly ISessionRepository _sessionRepository;
        private readonly IMessageBus _messageBus;
        public ProposalController(
            IValidatorServices validatorServices,
            ISessionRepository sessionRepository,
            IMessageBus messageBus)
            : base(validatorServices)
        {
            _sessionRepository = sessionRepository;
            _messageBus = messageBus;
        }



        [HttpPost]
        public async Task<IActionResult> Create(
            [FromHeader(Name = "correlation-id")] Guid correlationid,
            [FromHeader] Guid sessionId,
            [FromBody] CreateProposalRequest request)
        {
            var session = await _sessionRepository.GetByAggregateId(sessionId.ToString());

            if (session is null)
                return Unauthorized();

            var @event = new ProposalSentEvent(
                correlationid,
                request.Name,
                request.CPF,
                request.CNPJ,
                request.DDD,
                request.Cellphone);

            _messageBus.Publish(@event.Exchange, @event.RouterKey, @event);

            await Task.Delay(3000);

            return Accepted();
        }
    }
}

