using System;
using Google.Protobuf.WellKnownTypes;
using System.Runtime.Serialization;

namespace Atividade04.BFF.Models.Events
{
    [DataContract]
    public class ProposalStatusChangedEvent 
    {
      

        [DataMember]
        public string AggregateId
        {
            get;
              set;
        }

        [DataMember]
        public DateTime CreatedAt
        {
            get;
              set;
        }


        [DataMember]
        public DateTime? UpdatedAt
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

