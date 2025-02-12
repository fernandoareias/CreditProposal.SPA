﻿using System;
using System.Runtime.Serialization;
using Atividade02.Core.Common.CQRS;

namespace Atividade02.BFF.Models.Events
{
    [DataContract]
    public class ProposalSentEvent : Event
    {
        public ProposalSentEvent(
            Guid aggregateId,
            string name,
            string cpf,
            string cnpj,
            string ddd,
            string cellphone 
        ) : base("proposals", "proposal-sent")
        {
            AggregateId = aggregateId;
            Name = name;
            CPF = cpf;
            CNPJ = cnpj;
            DDD = ddd;
            Cellphone = cellphone;
        }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string CPF { get; private set; }

        [DataMember]
        public string CNPJ { get; private set; }

        [DataMember]
        public string DDD { get; private set; }

        [DataMember]
        public string Cellphone { get; private set; }

        [DataMember]
        public string? Notes { get; private set; }
    }
}

