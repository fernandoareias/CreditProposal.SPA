using System;
using System.Runtime.Serialization;
using Atividade02.Core.Common;
using Atividade04.BFF.Models;

namespace Atividade04.BFF.DTOs.Responses
{
    [DataContract]
    public class SignUpResponse
    {
        public SignUpResponse(string token, string name, string email, ERole role, string store)
        {
            Token = token;
            Name = name;
            Email = email;
            Role = Utils.GetEnumDescription(role);
            Store = store;
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
        public string Name
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

        [DataMember]
        public string Store { get; private set; }
    }
}

