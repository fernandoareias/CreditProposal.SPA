using System;
using System.Runtime.Serialization;

namespace Atividade04.BFF.DTOs.Requests
{
    [DataContract]
    public class SignInRequest
    {
        [DataMember]
        public string Email{
            get;
            set;
        }

        [DataMember]
        public string Password{
            get;
            set;
        }
    }
}

