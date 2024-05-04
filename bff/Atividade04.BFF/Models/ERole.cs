using System;
using System.ComponentModel;

namespace Atividade04.BFF.Models
{
    public enum ERole
    {
        [Description("Sales Associete")]
        SalesAssociete = 1,

        [Description("Manager")]
        Manager = 2,

        [Description("Owner")]
        Owner = 3
    }
}

