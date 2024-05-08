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

        public RetailerController(IValidatorServices validatorServices, IRetailerServices retailerServices, IAuthenticationServices authenticationServices) : base(validatorServices)
        {
            _retailerServices = retailerServices;
            _authenticationServices = authenticationServices;
        }
         

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] UpdateRetalierRequest request)
        {
            try
            {
                string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (string.IsNullOrWhiteSpace(token)) return Unauthorized();

                var email = _authenticationServices.ObterClaim(token, "email");

                if (string.IsNullOrWhiteSpace(email)) return Unauthorized();

                await _retailerServices.Update(email, request);

                return Ok();
            }
            catch (NullReferenceException)
            {
                return Unauthorized();
            }
        }


        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            try
            {
                string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (string.IsNullOrWhiteSpace(token)) return Unauthorized();

                var email = _authenticationServices.ObterClaim(token, "email");

                if (string.IsNullOrWhiteSpace(email)) return Unauthorized();

                await _retailerServices.Delete(email);

                return Ok();
            }
            catch (NullReferenceException)
            {
                return Unauthorized();
            }
        }



    }
}

