

namespace WebApi.DotNet.Sample.Helpers.ValueObjects
{
    public readonly struct Cpf
    {
        public readonly string Value;
        public readonly bool IsValid;
        private static readonly int[] Multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static readonly int[] Multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        public Cpf(string value)
        {
            Value = value;

            string tempCpf;
            string digito;
            int soma;
            int resto;

            value = value.Trim();
            value = value.Replace(".", "").Replace("-", "");

            if (value.Length != 11)
                IsValid = false;

            tempCpf = value.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * Multiplicador1[i];
            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf += digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * Multiplicador2[i];
            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();

            IsValid = value.EndsWith(digito);


        }

        public static implicit operator Cpf(string value)
        {
            return new(value);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}