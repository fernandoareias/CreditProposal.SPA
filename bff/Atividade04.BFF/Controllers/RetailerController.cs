using System;
using Atividade02.Core.Common.Validators.Interfaces;
using Atividade02.Core.Mediator.Interfaces;
using Atividade04.BFF.DTOs.Retailer.v1.Requests;
using Atividade04.BFF.DTOs.Retailer.v1.Responses;
using Atividade04.BFF.Models.Interfaces;
using Atividade04.BFF.Models.Interfaces.Services;
using Atividade04.BFF.Services;
using Microsoft.AspNetCore.Mvc;

namespace Atividade04.BFF.Controllers
{
    [ApiController]
    [Route("retailers")]
    public class RetailerController : CommonController
    {

        private readonly IRetailerServices _retailerServices;
        private readonly ISessionRepository _sessionRepository;
        private readonly IAuthenticationServices _authenticationServices;

        public RetailerController(IMediatorHandler mediatorHandler, IValidatorServices validatorServices, IRetailerServices retailerServices, IAuthenticationServices authenticationServices) : base(mediatorHandler, validatorServices)
        {
            _retailerServices = retailerServices;
            _authenticationServices = authenticationServices;
        }


    }
}

