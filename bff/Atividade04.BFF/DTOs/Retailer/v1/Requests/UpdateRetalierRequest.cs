using System;
using System.Runtime.Serialization;

namespace Atividade04.BFF.DTOs.Retailer.v1.Requests
{
    [DataContract]
    public class UpdateRetalierRequest
    {

        [DataMember]
        public string Name { get; set; }
    }
}

