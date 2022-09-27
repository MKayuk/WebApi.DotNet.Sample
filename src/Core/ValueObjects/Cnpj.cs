using System.Linq;

namespace WebApi.DotNet.Sample.Helpers.ValueObjects
{
    public readonly struct Cnpj
    {
        public readonly string Value;

        public readonly bool IsValid;
        private static readonly int[] Multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static readonly int[] Multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        public Cnpj(string value)
        {
            Value = value;
            IsValid = false;

            var digitosIdenticos = true;
            var ultimoDigito = -1;
            var posicao = 0;
            var totalDigito1 = 0;
            var totalDigito2 = 0;

            foreach (var digito in from c in Value where char.IsDigit(c) select c - '0')
            {
                digitosIdenticos = digitosIdenticos && DigitosIdenticos(posicao, ultimoDigito, digito);
                ultimoDigito = digito;

                switch (posicao)
                {
                    case < 12:
                        PosicaoMenorQue12(digito, posicao, ref totalDigito1, ref totalDigito2);
                        break;
                    case 12:
                        IsValid = PosicaoIgual12(digito, ref totalDigito1, ref totalDigito2);
                        break;
                    case 13:
                        IsValid = PosicaoIgual13(digito, ref totalDigito2);
                        break;
                }

                posicao++;
            }

            IsValid = IsValid && posicao == 14 && !digitosIdenticos;
        }

        private static bool DigitosIdenticos(int posicao, int ultimoDigito, int digito)
        {
            return posicao == 0 || ultimoDigito == digito;
        }

        private static void PosicaoMenorQue12(int digito, int posicao, ref int totalDigito1, ref int totalDigito2)
        {
            totalDigito1 += digito * Multiplicador1[posicao];
            totalDigito2 += digito * Multiplicador2[posicao];
        }

        private static bool PosicaoIgual12(int digito, ref int totalDigito1, ref int totalDigito2)
        {
            var dv1 = totalDigito1 % 11;
            dv1 = dv1 < 2 ? 0 : 11 - dv1;

            if (digito != dv1)
                return false;

            totalDigito2 += dv1 * Multiplicador2[12];
            return true;
        }

        private static bool PosicaoIgual13(int digito, ref int totalDigito2)
        {
            var dv2 = totalDigito2 % 11;
            dv2 = dv2 < 2 ? 0 : 11 - dv2;
            return digito == dv2;
        }

        public static implicit operator Cnpj(string value)
        {
            return new(value);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}