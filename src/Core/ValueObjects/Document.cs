using WebApi.DotNet.Sample.Helpers.ValueObjects;
using System.Linq;

namespace WebApi.DotNet.Sample.Helpers.ValueObjects
{
    public class Document : ValueObject<Document>
    {
        public readonly string Value;
        public readonly bool IsValid;

        public Document(string document)
        {
            if (string.IsNullOrEmpty(document))
            {
                IsValid = false;
                return;
            }

            Value = document.Where(char.IsDigit).Aggregate(string.Empty, (current, t) => current + t);

            if (Value.Length == 11)
                IsValid = new Cpf(Value).IsValid;
            if (Value.Length == 14)
                IsValid = new Cnpj(Value).IsValid;
        }

        public static explicit operator Document(string value)
        {
            return new(value);
        }

        public static implicit operator string(Document document)
        {
            return document.Value;
        }

        public override string ToString()
        {
            return Value;
        }

        protected override bool EqualsCore(Document other)
        {
            if (other is null)
                return false;

            if (other is { } obj)
                return Value == obj.Value;

            return false;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}