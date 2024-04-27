using System;
using System.Runtime.Serialization;
using Atividade02.Core.Common.CQRS;

namespace Atividade02.Proponent.API.Events
{
    [DataContract]
    public class ProposalApprovedEvent : Event
    {
        protected ProposalApprovedEvent() : base("proposals", "proposals-approved")
        {
        }

        public ProposalApprovedEvent(string aggregateId, string code, ProposalApprovedProponentEvent proponent) : base("proposals", "proposals-approved")
        {
            AggregateId = aggregateId;
            Code = code;
            Proponent = proponent;
        }

        [DataContract]
        public class ProposalApprovedProponentEvent
        {
            public ProposalApprovedProponentEvent(string name, string cPF, string dDD, string cellphoneNumber)
            {
                Name = name;
                CPF = cPF;
                DDD = dDD;
                CellphoneNumber = cellphoneNumber;
            }

            protected ProposalApprovedProponentEvent()
            {

            }


            [DataMember]
            public string Name
            {
                get;
                init;
            }

            [DataMember]
            public string CPF
            {
                get;
                init;
            }

            [DataMember]
            public string DDD
            {
                get;
                init;
            }

            [DataMember]
            public string CellphoneNumber
            {
                get;
                init;
            }
        }

        [DataMember]
        public string AggregateId
        {
            get;
            init;
        }

        [DataMember]
        public string Code
        {
            get;
            init;
        }

        [DataMember]
        public ProposalApprovedProponentEvent Proponent
        {
            get;
            init;
        }
    }
}

