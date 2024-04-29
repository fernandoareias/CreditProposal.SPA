using System;
using Atividade04.BFF.DTOs.Requests;
using Atividade04.BFF.DTOs.Responses;
using Atividade04.BFF.Models;

namespace Atividade04.BFF.Services
{
    public interface IAuthenticationServices
    {
        Output? DecryptMessage<Output>(string encryptedMessage, string publicKey, string privateKey) where Output : class;
        (string privateKey, string publicKey) GenerateKeys();
        Task<SignResponse> SignIn(Session session, SignInRequest request);
        Task<SignUpResponse> SignUp(Session session, SignUpRequest request);
    }
}

