using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WebApi.DotNet.Sample.Helpers.ValueObjects
{
    public class Email
    {
        public readonly string Value;
        public readonly bool IsValid;

        public Email(string value)
        {
            Value = value;
            IsValid = IsEmail(value);
        }

        private static bool IsEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

                static string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();
                    var domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static explicit operator Email(string value)
        {
            return new(value);
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}