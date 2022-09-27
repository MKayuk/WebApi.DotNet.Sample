using System.Linq;

namespace WebApi.DotNet.Sample.Helpers.ValueObjects
{
    public class ZipCode : ValueObject<ZipCode>
    {
        public readonly string Value;

        public ZipCode(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            Value = value.Where(char.IsDigit).Aggregate(string.Empty, (current, t) => current + t);
        }

        public static explicit operator ZipCode(string value)
        {
            return new(value);
        }

        public static implicit operator string(ZipCode zipCode)
        {
            return zipCode.Value;
        }

        public override string ToString()
        {
            return Value;
        }

        protected override bool EqualsCore(ZipCode other)
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