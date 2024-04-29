using System;
using Atividade02.Core.Common.CQRS;

namespace Atividade04.BFF.Models
{
    public class Retailer : AggregateRoot
    {
        protected Retailer()
        {
        }

        public Retailer(string name, string email, string password, string cnpj, ERole role)
        {
            Name = name;
            Email = email;
            Password = password;
            CNPJ = cnpj;
            Role = role;
        }

        public string Name{
            get;
            private set;
        }

        public string Email{
            get;
            private set;
        }

        public string Password {
            get;
            private set;
        }

        public string CNPJ{
            get;
            private set;
        }

        public ERole Role{
            get;
            private set;
        }


        public List<Session> Sessions
        {
            get;
            private set;
        }
    }
}

