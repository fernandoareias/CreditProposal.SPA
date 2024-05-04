using System;
using System.Runtime.Serialization;

namespace Atividade04.BFF.DTOs.Retailer.v1.Responses
{
    [DataContract]
    public class CreateRetalierResponse
    {
        protected CreateRetalierResponse()
        {

        }

        public CreateRetalierResponse(string token, string email, string role)
        {
            Token = token;
            Email = email;
            Role = role;
        }

        [DataMember]
        public string Token{
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

