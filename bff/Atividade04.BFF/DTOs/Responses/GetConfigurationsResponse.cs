using System;
using System.Runtime.Serialization;

namespace Atividade04.BFF.DTOs.Responses
{
    [DataContract]
    public class GetConfigurationsResponse
    {
        protected GetConfigurationsResponse()
        {

        }

        public GetConfigurationsResponse(string version, string publicKey)
        {
            Version = version;
            PublicKey = publicKey;
        }

        [DataMember]
        public string Version{
            get;
            private set;
        }

        [DataMember]
        public string PublicKey{
            get;
            private set;
        }
    }
}

