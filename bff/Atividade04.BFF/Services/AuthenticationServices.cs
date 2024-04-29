using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Atividade02.Core.Common;
using Atividade04.BFF.Configurations;
using Atividade04.BFF.DTOs.Requests;
using Atividade04.BFF.DTOs.Responses;
using Atividade04.BFF.Models;
using Atividade04.BFF.Models.Interfaces.Repositories;
using Microsoft.Extensions.Options;

namespace Atividade04.BFF.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly RsaAppConfiguration _rsaConfiguration;
        private readonly IRetailerRepository _retailerRepository;

        public AuthenticationServices(IOptions<RsaAppConfiguration> rsaConfiguration, IRetailerRepository retailerRepository)
        {
            _rsaConfiguration = rsaConfiguration.Value;
            _retailerRepository = retailerRepository;
        }

        public Output? DecryptMessage<Output>(string encryptedMessage, string publicKey, string privateKey) where Output : class
        {
            byte[] privateKeyBytes = Convert.FromBase64String(privateKey);

            using var rsa = new RSACryptoServiceProvider(_rsaConfiguration.KeySize); 
            
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
            using var vRSA = new RSACryptoServiceProvider(_rsaConfiguration.KeySize);

            var publicKeyBytes = vRSA.ExportRSAPublicKey();

            var privateKeyBytes = vRSA.ExportRSAPrivateKey();

            var publicKeyBase64String = CustomConverter.ByteArrayToBase64String(publicKeyBytes);
            var privateKeyBase64String = CustomConverter.ByteArrayToBase64String(privateKeyBytes);

            return (publicKeyBase64String, privateKeyBase64String);
        }

        public async Task<SignResponse> SignIn(Session session, SignInRequest request)
        {
            var retalier = await _retailerRepository.GetByEmail(request.Email);
        }

        public Task<SignUpResponse> SignUp(Session session, SignUpRequest request)
        {
            throw new NotImplementedException();
        }
    }
    
}

