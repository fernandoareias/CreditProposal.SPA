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
using Newtonsoft.Json;

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
                System.Text.Json.JsonSerializer.Deserialize<Output>(content) : null;
            
        }

        public (string privateKey, string publicKey) GenerateKeys()
        {
            using var vRSA = new RSACryptoServiceProvider(authenticationConfiguration.KeySize);

            var publicKeyBytes = vRSA.ExportRSAPublicKey();

            var privateKeyBytes = vRSA.ExportRSAPrivateKey();

            var publicKeyBase64String = CustomConverter.ByteArrayToBase64String(publicKeyBytes);
            var privateKeyBase64String = CustomConverter.ByteArrayToBase64String(privateKeyBytes);

            return (privateKeyBase64String, publicKeyBase64String);
        }

        public async Task<SignResponse> SignIn(Session session, SignInRequest request, string signature)
        {
            if (!VerifySignature(session.PublicKey, session.AggregateId, request, signature))
                return null;

            var retalier = await _retailerRepository.GetByEmail(request.Email);

            if (retalier is null) return null;

            if (!retalier.Password.Compare(request.Password)) return null;

            retalier.Authenticated(session);

            _retailerRepository.Update(retalier);
            await _retailerRepository.unitOfWork.Commit();

            var token = GenerateJwt(session, retalier);

            return new SignResponse(token, retalier.Name, retalier.Email.Address, retalier.Role, retalier.CNPJ.Number);
        }

        private static bool VerifySignature(string publicKey, string sessionId, SignInRequest data, string signature)
        {
            try
            {
                string stringAssinada = sessionId + data.Email + data.Password;
                // Serializa o objeto SignInRequest em JSON
                byte[] dataBytes = Encoding.UTF8.GetBytes(stringAssinada);

                // Converte a assinatura de base64 para bytes
                byte[] signatureBytes = Convert.FromBase64String(signature);
                byte[] publicKeyBytes = Convert.FromBase64String(publicKey);

                // Cria uma instância da classe RSACryptoServiceProvider
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportRSAPublicKey(publicKeyBytes, out _);

                    // Verifica a assinatura
                    return  rsa.VerifyData(dataBytes, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }
            }
            catch (CryptographicException e)
            {
                Console.WriteLine($"Erro ao verificar a assinatura: {e.Message}");
                return false;
            }
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

            return new SignUpResponse(token, retalier.Name, retalier.Email.Address, retalier.Role, retalier.CNPJ.Number);
        }
         
         
        public string GenerateJwt(Session session, Retailer retailer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(authenticationConfiguration.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, retailer.AggregateId.ToString()),
                    new Claim("Session", session.AggregateId),
                    new Claim(ClaimTypes.Name, retailer.Name),
                    new Claim(ClaimTypes.Email, retailer.Email.Address),
                    new Claim(ClaimTypes.Role, retailer.Role.ToString()),
                    new Claim("Store", retailer.CNPJ.Number)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
    
}

