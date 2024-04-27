using System;
using Atividade02.Core.Common.CQRS;
using MongoDB.Bson.Serialization.Attributes;

namespace Atividade02.Proponent.API.Models
{
    public class Portador : AggregateRoot
    {
        public Portador()
        {

        }

        public Portador(string aggrgateId, string name, string cpf, string ddd, string number)
        {
            AggregateId = aggrgateId;
            Name = name;
            CPF = cpf;
            CellphoneDDD = ddd;
            CellphoneNumber = number;
        }

        [BsonElement("Name")]
        public string Name
        {
            get;
            private set;
        }

        [BsonElement("CPF")]
        public string CPF
        {
            get;
            private set;
        }

        [BsonElement("CellphoneDDD")]
        public string CellphoneDDD
        {
            get;
            private set;
        }

        [BsonElement("CellphoneNumber")]
        public string CellphoneNumber
        {
            get;
            private set;
        }
    }
}

