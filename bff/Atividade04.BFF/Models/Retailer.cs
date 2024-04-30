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
            Email = new Email(email);
            Password = new Password(password);
            CNPJ = new CNPJ(cnpj);
            Role = role;
        }

        public string Name{
            get;
            private set;
        }

        public Email Email{
            get;
            private set;
        }

        public Password Password {
            get;
            private set;
        }

        public CNPJ CNPJ{
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


        public void Authenticated(Session session)
        {
            Sessions.Add(session);
        }
    }
}

