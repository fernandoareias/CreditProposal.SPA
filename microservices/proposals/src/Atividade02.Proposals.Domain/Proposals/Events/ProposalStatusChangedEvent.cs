using System;
using System.Runtime.Serialization;
using Atividade02.Core.Common.CQRS;

namespace Atividade02.Proposals.Domain.Proposals.Events
{
    [DataContract]
    public class ProposalStatusChangedEvent : Event
    {
        protected ProposalStatusChangedEvent() : base("proposals", "proposals-status-changed")
        {

        }

        public ProposalStatusChangedEvent(Proposal proposal) : base("proposals", "proposals-status-changed")
        {
            AggregateId = proposal.AggregateId;
            CreatedAt = proposal.CreatedAt;
            UpdatedAt = proposal.UpdatedAt;
            Code = proposal.Code;
            Name = proposal.Proponent.Name.Value;
            CPF = proposal.Proponent.CPF.Number;
            Cellphone = proposal.Proponent.Cellphone.ToString();
            Status = proposal.Status.ToString();
            CreditLimit = proposal.Status == Enums.EProposalStatus.APPROVED ? new Random().Next(100, 1001).ToString() : null;
        }

        [DataMember]
        public string AggregateId
        {
            get;
            private set;
        }

        [DataMember]
        public DateTime CreatedAt{
            get;
            private set;
        }


        [DataMember]
        public DateTime? UpdatedAt
        {
            get;
            private set;
        }

        [DataMember]
        public string Code{
            get;
            private set;
        }

        [DataMember]
        public string Name{
            get;
            private set;
        }

        [DataMember]
        public string CPF{
            get;
            private set;
        }


        [DataMember]
        public string Cellphone{
            get;
            private set;
        }

        [DataMember]
        public string Status
        {
            get;
            private set;
        }

        [DataMember]
        public string? CreditLimit
        {
            get;
            private set;
        }
    }
}

