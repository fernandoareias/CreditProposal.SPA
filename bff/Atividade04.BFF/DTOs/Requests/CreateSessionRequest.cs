using System;
using System.Runtime.Serialization;

namespace Atividade04.BFF.DTOs.Requests
{
    [DataContract]
    public class CreateSessionRequest
    {

        [DataMember]
        public string Key { get; set; }
    }
}

