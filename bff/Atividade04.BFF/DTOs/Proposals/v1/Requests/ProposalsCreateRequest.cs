using System;
using System.Runtime.Serialization;

namespace Atividade04.BFF.DTOs.Proposals.v1.Requests
{
    [DataContract]
    public class CreateProposalRequest
    {

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string CPF { get; set; }

        [DataMember]
        public string CNPJ { get; set; }

        [DataMember]
        public string DDD { get; set; }

        [DataMember]
        public string Cellphone { get; set; }

    }
}

