using System;
using System.Security.Cryptography;
using Atividade02.Core.Common;
using Atividade02.Core.Common.Validators.Interfaces;
using Atividade02.Core.Mediator.Interfaces;
using Atividade04.BFF.Configurations;
using Atividade04.BFF.DTOs.Requests;
using Atividade04.BFF.DTOs.Responses;
using Atividade04.BFF.Models;
using Atividade04.BFF.Models.Interfaces;
using Atividade04.BFF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Atividade04.BFF.Controllers
{
    [ApiController]
    [Route("authentication")]
    public class AuthenticationController : CommonController
    {
        private readonly IConfiguration _configuration;
        private readonly RsaAppConfiguration _rsaConfiguration;
        private readonly ISessionRepository _sessionRepository;
        private readonly IAuthenticationServices _authenticationServices;

        public AuthenticationController(
            IValidatorServices validatorServices,
            IConfiguration configuration,
            IOptions<RsaAppConfiguration> rsaConfiguration
,
            ISessionRepository sessionRepository,
            IAuthenticationServices authenticationServices)
            : base(validatorServices)
        {
            _configuration = configuration;
            _rsaConfiguration = rsaConfiguration.Value;
            _sessionRepository = sessionRepository;
            _authenticationServices = authenticationServices;
        }


        /// <summary>
        /// Cria a sessao
        /// </summary>
        /// <returns></returns>
        [HttpPost("session")]
        public async Task<IActionResult> CreateSession([FromHeader]Guid sessionId)
        {
            (string privateKey, string publicKey) = _authenticationServices.GenerateKeys();

            var session = new Session(sessionId, _configuration["Application:Version"], GetIpAddress(HttpContext),  privateKey, publicKey);


            _sessionRepository.Add(session);
            await _sessionRepository.unitOfWork.Commit();

            return Ok(new GetConfigurationsResponse(session.Version, publicKey));
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromHeader] Guid sessionId, [FromBody] string request)
        {
            var session = await _sessionRepository.GetByAggregateId(sessionId.ToString());

            if (session is null)
                return Unauthorized();

            var requestObject = _authenticationServices.DecryptMessage<SignInRequest>(request, session.PublicKey, session.PrivateKey);

            if(requestObject is null)
                return Unauthorized();


        }
    }
}

