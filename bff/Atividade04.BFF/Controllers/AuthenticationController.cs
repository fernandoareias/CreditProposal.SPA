using System;
using Atividade02.Core.Common.Validators.Interfaces;
using Atividade02.Core.Mediator.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Atividade04.BFF.Controllers
{
    [ApiController]
    [Route("proposals")]
    public class AuthenticationController : CommonController
    {
        public AuthenticationController(IMediatorHandler mediatorHandler, IValidatorServices validatorServices) : base(mediatorHandler, validatorServices)
        {
        }


    }
}

