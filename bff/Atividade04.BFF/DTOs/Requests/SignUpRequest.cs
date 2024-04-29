using System;
using System.Runtime.Serialization;

namespace Atividade04.BFF.DTOs.Requests
{
    [DataContract]
    public class SignUpRequest
    {

        protected SignUpRequest()
        {

        }

        public SignUpRequest(string name, string email, string password, string confirmPassword)
        {
            Name = name;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        [DataMember]
        public string Name
        {
            get;
            set;
        }

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

        [DataMember]
        public string ConfirmPassword
        {
            get;
            set;
        }


    }
}

