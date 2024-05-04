using System;
using Atividade04.BFF.Models.Common;

namespace Atividade04.BFF.Models
{
    public class CNPJ : Document
    { 
        public CNPJ(string number) 
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("CNPJ number cannot be null or empty.", nameof(Number));

            Number = new string(number.Where(char.IsDigit).ToArray());

            if (Number.Length != 14)
                throw new ArgumentException("CNPJ number must contain 14 numeric digits.", nameof(Number));

            if (!IsValid())
                throw new ArgumentException("The Informed CNPJ is invalid.", nameof(Number));
        }

        public override string GetNumberWithMask()
        {
            return string.Format("{0}.{1}.{2}/{3}-{4}",
                Number.Substring(0, 2),
                Number.Substring(2, 3),
                Number.Substring(5, 3),
                Number.Substring(8, 4),
                Number.Substring(12, 2));
        }

        public override bool IsValid()
        {
            int[] multiplicadores1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = Number.Substring(0, 12);

            int soma = 0;
            for (int i = 0; i < 12; i++)
            {
                soma += (tempCnpj[i] - '0') * multiplicadores1[i];
            }

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            tempCnpj += resto;

            soma = 0;
            for (int i = 0; i < 13; i++)
            {
                soma += (tempCnpj[i] - '0') * multiplicadores2[i];
            }

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            tempCnpj += resto;

            return Number.EndsWith(tempCnpj);
        }
    }
}

