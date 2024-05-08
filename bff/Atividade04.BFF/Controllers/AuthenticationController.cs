using System;
using System.Security.Cryptography;
using System.Text;
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
using ThirdParty.Json.LitJson;

namespace Atividade04.BFF.Controllers
{
    [ApiController]
    [Route("authentication")]
    public class AuthenticationController : CommonController
    {
        private readonly IConfiguration _configuration;
        private readonly ISessionRepository _sessionRepository;
        private readonly IAuthenticationServices _authenticationServices;

        public AuthenticationController(
            IValidatorServices validatorServices,
            IConfiguration configuration,
            ISessionRepository sessionRepository,
            IAuthenticationServices authenticationServices)
            : base(validatorServices)
        {
            _configuration = configuration;
            _sessionRepository = sessionRepository;
            _authenticationServices = authenticationServices;
        }


        /// <summary>
        /// Cria a sessao
        /// </summary>
        /// <returns></returns>
        [HttpPost("session")]
        public async Task<IActionResult> CreateSession([FromHeader] Guid sessionId, [FromBody] CreateSessionRequest request)
        {

            var sessao = await _sessionRepository.GetByAggregateId(sessionId.ToString());

            if (sessao is not null)
            {
                await Task.Delay(3000);
                return Ok(new GetConfigurationsResponse(sessao.Version, Criptografando(sessao.PrivateKey, request.Key)));
            }

            (string privateKey, string publicKey) = _authenticationServices.GenerateKeys();

            var session = new Session(sessionId, _configuration["Application:Version"], GetIpAddress(HttpContext), privateKey, publicKey);


            _sessionRepository.Add(session);
            await _sessionRepository.unitOfWork.Commit();

            await Task.Delay(3000);

            return Ok(new GetConfigurationsResponse(session.Version, Criptografando(privateKey, request.Key)));
        }


        private List<string> Criptografando(string privateKeyBackend, string publicKey)
        {
            List<string> blocosCriptografados = new List<string>();

            // Convertendo o JSON para bytes
            byte[] jsonDataBytes = Encoding.UTF8.GetBytes(privateKeyBackend);

            string publicKeyPEMBase64 = publicKey.Replace("-----BEGIN PUBLIC KEY-----", "")
                                                 .Replace("-----END PUBLIC KEY-----", "")
                                                 .Replace("\n", "")
                                                 .Replace("\r", "")
                                                 .Trim();

            byte[] publicKeyBytes = Convert.FromBase64String(publicKeyPEMBase64);

            // Criando uma instância de RSACryptoServiceProvider
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);
                // Dividindo a chave privada em blocos menores
                for (int i = 0; i < privateKeyBackend.Length; i += 100)
                {
                    string block = privateKeyBackend.Substring(i, Math.Min(100, privateKeyBackend.Length - i));
                    byte[] blockBytes = Encoding.UTF8.GetBytes(block);
                    // Criptografando o bloco atual usando a chave pública
                    byte[] encryptedBlockBytes = rsa.Encrypt(blockBytes, false);
                    string encryptedBlock = Convert.ToBase64String(encryptedBlockBytes);
                    // Adicionando o bloco criptografado à lista
                    blocosCriptografados.Add(encryptedBlock);
                }
            }

            return blocosCriptografados;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("sign-in")]
        public async Task<IActionResult> Login([FromHeader] Guid sessionId, [FromHeader] string Authorization, [FromBody] SignInRequest request)
        {
            var session = await _sessionRepository.GetByAggregateId(sessionId.ToString());

            if (session is null)
            {
                await Task.Delay(3000);
                return Unauthorized();
            }

            int index = Authorization.IndexOf("Bearer ") + "Bearer ".Length;

            // Extrai o token da string de cabeçalho de autorização
            string token = Authorization.Substring(index);

            var response = await _authenticationServices.SignIn(session, request, token);

            if (response is null)
            {
                await Task.Delay(3000);
                return Unauthorized();
            }

            await Task.Delay(3000);
            return Ok(response);
        }


        /// <summary>
        /// SignUp
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromHeader] Guid sessionId, [FromBody] SignUpRequest request)
        {
            var session = await _sessionRepository.GetByAggregateId(sessionId.ToString());

            if (session is null)
            {
                await Task.Delay(3000);
                return Unauthorized();
            }

            var response = await _authenticationServices.SignUp(session, request);

            if (response is null)
            {
                await Task.Delay(3000);
                return BadRequest("This email is already registered");
            }

            await Task.Delay(3000);
            return Ok(response);
        }
    }
}

