using System;
using Atividade02.Worker.Domain.Common;
using System.Text.RegularExpressions;

namespace Atividade04.BFF.Models
{
    public class Email : ValueObjects
    {
        public Email(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Email address cannot be null or empty.", nameof(address));

            Address = address;

            if (!IsValid())
                throw new ArgumentException("Invalid email address.", nameof(address));
        }

        public string Address { get; private set; }


        public override bool IsValid()
        {
            try
            {
                var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
                return regex.IsMatch(Address);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}

