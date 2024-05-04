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

        public GetConfigurationsResponse(string version, List<string> privateKey)
        {
            Version = version;
            PrivateKey = privateKey;
        }

        [DataMember]
        public string Version{
            get;
            private set;
        }

        [DataMember]
        public List<string> PrivateKey{
            get;
            private set;
        }
    }
}

