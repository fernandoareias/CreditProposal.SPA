using System;
using System.Runtime.Serialization;

namespace Atividade04.BFF.DTOs.Responses
{
    [DataContract]
    public class SignUpResponse
    {
        public SignUpResponse(string token, string email, string role)
        {
            Token = token;
            Email = email;
            Role = role;
        }

        protected SignUpResponse()
        {

        }

        [DataMember]
        public string Token
        {
            get;
            private set;
        }

        [DataMember]
        public string Email
        {
            get;
            private set;
        }

        [DataMember]
        public string Role
        {
            get;
            private set;
        }
    }
}

