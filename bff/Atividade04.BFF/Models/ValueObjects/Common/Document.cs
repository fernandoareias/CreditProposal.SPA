using System;
using Atividade02.Worker.Domain.Common;

namespace Atividade04.BFF.Models.Common
{
    public abstract class Document : ValueObjects
    {
        protected Document()
        {

        }


        public string Number
        {
            get;
            protected set;
        }

        public override string ToString()
        {
            return Number.ToString();
        }

        public virtual string GetNumberWithMask()
        {
            throw new NotImplementedException();
        }
    }
}

