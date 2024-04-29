using System;
using Atividade02.Core.Common.CQRS;
using Atividade02.Core.Common.Enums;
using Atividade02.Core.Common.Validators.Interfaces;
using Atividade02.Core.Mediator.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Atividade04.BFF.Controllers
{
    public class CommonController : ControllerBase
    {
        public CommonController(
            IMediatorHandler mediatorHandler,
            IValidatorServices validatorServices)
        {
            _mediatorHandler = mediatorHandler;
            _validatorServices = validatorServices;
        }

        public CommonController(
            IValidatorServices validatorServices)
        {
            _validatorServices = validatorServices;
        }

        protected readonly IValidatorServices _validatorServices;
        protected readonly IMediatorHandler _mediatorHandler;


        protected virtual bool HasError => _validatorServices.ValidationResult.Errors.Any();


        #region 2xxx

        public IActionResult ReturnOk<T>() where T : View
        => new OkObjectResult(new BaseResponse<T>(System.Net.HttpStatusCode.OK, $"Operation carried out successfully."));

        public IActionResult ReturnOk<T>(T view) where T : View
         => new OkObjectResult(new BaseResponse<T>(System.Net.HttpStatusCode.OK, $"Operation carried out successfully.", view));


        public IActionResult ReturnOkWithList<T>(List<T> view) where T : View
            => new OkObjectResult(new BaseListResponse<T>(System.Net.HttpStatusCode.OK, "Operation carried out successfully.", view!));

        public IActionResult ReturnOkWithEmptyList<T>() where T : View
           => new OkObjectResult(new BaseListResponse<T>(System.Net.HttpStatusCode.OK, "Operation carried out successfully."));

        public IActionResult ReturnNotFound<T>(string message) where T : View
            => new NotFoundObjectResult(new BaseResponse<T>(System.Net.HttpStatusCode.NotFound, message));
        #endregion

        #region 4xx

        public IActionResult ReturnBadRequestWithErrors<T>(string codigo, string grupoErro) where T : View
            => new BadRequestObjectResult(new BaseResponse<T>(System.Net.HttpStatusCode.BadRequest, _validatorServices.ValidationResult.Errors.Select(c => new ResponseErroView(codigo, grupoErro, c.ErrorMessage)).ToList()));


        public IActionResult ReturnBadRequestWithErrors<T>() where T : View
             => new BadRequestObjectResult(new BaseResponse<T>(System.Net.HttpStatusCode.BadRequest, _validatorServices.ValidationResult.Errors.Select(c => new ResponseErroView(EBaseErro.INVALID_FIELD.ToString(), "BAD_REQUEST", c.ErrorMessage)).ToList()));

        #endregion

         
        protected string GetIpAddress(HttpContext context)
        {
            // Verifica se há um proxy de cliente definido no cabeçalho HTTP
            string? ip = context!.Connection?.RemoteIpAddress?.ToString();

            // Se houver um proxy de cliente, use-o para obter o endereço IP real
            if (context!.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                ip = context.Request.Headers["X-Forwarded-For"];
            }

            return ip!;
        }
    }
}

