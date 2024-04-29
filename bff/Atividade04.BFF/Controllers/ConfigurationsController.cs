using System;
using Atividade02.Core.Common.Validators.Interfaces;
using Atividade02.Core.Mediator.Interfaces;
using Atividade04.BFF.Configurations;
using Atividade04.BFF.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Atividade04.BFF.Controllers
{
    [ApiController]
    [Route("configurations")]
    public class ConfigurationsController : CommonController
    {
        private readonly IConfiguration _configuration;
        private readonly RsaAppConfiguration _rsaConfiguration;

        public ConfigurationsController(
            IMediatorHandler mediatorHandler,
            IValidatorServices validatorServices,
            IConfiguration configuration,
            IOptions<RsaAppConfiguration> rsaConfiguration
        )
            : base(mediatorHandler, validatorServices)
        {
            _configuration = configuration;
            _rsaConfiguration = rsaConfiguration.Value;
        }


    }
}

