using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Atividade02.Core.Common;
using Atividade04.BFF.Configurations;
using Atividade04.BFF.DTOs.Requests;
using Atividade04.BFF.DTOs.Responses;
using Atividade04.BFF.Models;
using Atividade04.BFF.Models.Interfaces.Repositories;
using Elastic.CommonSchema;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Atividade04.BFF.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly AuthenticationConfiguration authenticationConfiguration;
        private readonly IRetailerRepository _retailerRepository;

        public AuthenticationServices(IOptions<AuthenticationConfiguration> authenticationConfiguration, IRetailerRepository retailerRepository)
        {
            this.authenticationConfiguration = authenticationConfiguration.Value;
            _retailerRepository = retailerRepository;
        }

        public Output? DecryptMessage<Output>(string encryptedMessage, string publicKey, string privateKey) where Output : class
        {
            byte[] privateKeyBytes = Convert.FromBase64String(privateKey);

            using var rsa = new RSACryptoServiceProvider(authenticationConfiguration.KeySize); 
            
            rsa.ImportRSAPrivateKey(privateKeyBytes, out _);

            byte[] encryptedBytes = Convert.FromBase64String(encryptedMessage);

            byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, true);

            var content = Encoding.UTF8.GetString(decryptedBytes);
            
            return !string.IsNullOrWhiteSpace(content)
                ?
                JsonSerializer.Deserialize<Output>(content) : null;
            
        }

        public (string privateKey, string publicKey) GenerateKeys()
        {
            using var vRSA = new RSACryptoServiceProvider(authenticationConfiguration.KeySize);

            var publicKeyBytes = vRSA.ExportRSAPublicKey();

            var privateKeyBytes = vRSA.ExportRSAPrivateKey();

            var publicKeyBase64String = CustomConverter.ByteArrayToBase64String(publicKeyBytes);
            var privateKeyBase64String = CustomConverter.ByteArrayToBase64String(privateKeyBytes);

            return (publicKeyBase64String, privateKeyBase64String);
        }

        public async Task<SignResponse> SignIn(Session session, SignInRequest request)
        {
            var retalier = await _retailerRepository.GetByEmail(request.Email);

            if (retalier is null) return null;

            if (!retalier.Password.Compare(request.Password)) return null;

            retalier.Authenticated(session);

            _retailerRepository.Add(retalier);
            await _retailerRepository.unitOfWork.Commit();

            var token = GenerateJwt(session, retalier);

            return new SignResponse(token, retalier.Email.Endereco, retalier.Role.ToString());
        }

        public async Task<SignUpResponse> SignUp(Session session, SignUpRequest request)
        {
            var retalier = await _retailerRepository.GetByEmail(request.Email);

            if (retalier is not null) return null;

            retalier = new Retailer(request.Name, request.Email, request.Password, request.CNPJ, ERole.SalesAssociete);

            retalier.Authenticated(session);

            _retailerRepository.Add(retalier);
            await _retailerRepository.unitOfWork.Commit();

            var token = GenerateJwt(session, retalier);

            return new SignUpResponse(token, retalier.Email.Endereco, retalier.Role.ToString());
        }
         
        private string? GetClaimValue(HttpContext context, string claim)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            if (identity != null)
                return identity?.FindFirst(claim)?.Value;

            return null;

        }


        private string GenerateJwt(Session session, Retailer retailer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(authenticationConfiguration.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, retailer.AggregateId.ToString()),
                    new Claim("Session", session.AggregateId),
                    new Claim(ClaimTypes.Name, retailer.Email.Endereco),
                    new Claim(ClaimTypes.Role, retailer.Role.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
    
}

