using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Atividade02.Core.Common.CQRS;

namespace Atividade04.BFF.DTOs.Requests
{
    [DataContract]
    public class GetProposalListResponse
    {

        [DataMember]
        public List<GetProposalListItemResponse> Proposals { get; set; } = new List<GetProposalListItemResponse>();
    }

    [DataContract]
    public class GetProposalListItemResponse
    {
        [DataMember]
        public string AggregateId
        {
            get;
            set;
        }

        [DataMember]
        public string CreatedAt
        {
            get;
            set;
        }


        [DataMember]
        public string? UpdatedAt
        {
            get;
            set;
        }

        [DataMember]
        public string Code
        {
            get;
            set;
        }

        [DataMember]
        public string Name
        {
            get;
            set;
        }

        [DataMember]
        public string CPF
        {
            get;
            set;
        }


        [DataMember]
        public string Cellphone
        {
            get;
            set;
        }

        [DataMember]
        public string Status
        {
            get;
            set;
        }

        [DataMember]
        public string? CreditLimit
        {
            get;
            set;
        }


    }
}