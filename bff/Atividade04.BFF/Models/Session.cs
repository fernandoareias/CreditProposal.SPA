using System;
using Atividade02.Core.Common.CQRS;
using MongoDB.Bson.Serialization.Attributes;

namespace Atividade04.BFF.Models
{
    public class Session : AggregateRoot
    {
        public Session()
        {

        }

        public Session(Guid aggregateId, string version, string ip, string privateKey, string publicKey)
        {
            AggregateId = aggregateId.ToString();
            PrivateKey = privateKey;
            PublicKey = publicKey;
            Version = version;
            IP = ip;
        }

        public Session(Guid aggregateId, string version, string privateKey, string publicKey)
        {
            AggregateId = aggregateId.ToString();
            Version = version;
            PrivateKey = privateKey;
            PublicKey = publicKey;
        }

        [BsonElement]
        public string IP
        {
            get;
            private set;
        }

        [BsonElement]
        public string Version{
            get;
            private set;
        }

        [BsonElement("PrivateKey")]
        public string PrivateKey{
            get;
            private set;
        }

        [BsonElement("PublicKey")]
        public string PublicKey{
            get;
            private set;
        }

    }
}

