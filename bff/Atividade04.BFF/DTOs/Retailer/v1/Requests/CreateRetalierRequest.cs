using System;
using System.Runtime.Serialization;

namespace Atividade04.BFF.DTOs.Retailer.v1.Requests
{
    [DataContract]
    public class CreateRetalierRequest
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string CNPJ { get; set; }
    }
}

