using System;
using System.Runtime.Serialization;

namespace WebApi.DotNet.Sample.Helpers.ValueObjects
{
#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    public class Uf : ValueObject<Uf>, ISerializable
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
    {
        private readonly string _value;

        public static readonly string[] Ufs =
        {
            "AC", "AL", "PA", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI",
            "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO"
        };

        public Uf(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException($"Invalid argument {nameof(value)}");

            for (var i = 0; i <= Ufs.Length - 1; i++)
            {
                if (Ufs[i] != value.ToUpper())
                    continue;

                _value = Ufs[i];
                return;
            }

            throw new ArgumentException($"Invalid Uf {value}");
        }

        public Uf(SerializationInfo info, StreamingContext context)
        {
            _value = (string)info.GetValue("props", typeof(string));
        }

        public static explicit operator Uf(string value)
        {
            return new(value);
        }

        public static implicit operator string(Uf uf)
        {
            return uf._value;
        }

        public override string ToString() => _value;

        protected override bool EqualsCore(Uf other)
        {
            if (other is null)
                return false;

            if (other is { } obj)
                return _value == obj._value;

            return false;
        }

        protected override int GetHashCodeCore()
        {
            return _value.GetHashCode();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("props", _value, typeof(string));
        }
    }
}
