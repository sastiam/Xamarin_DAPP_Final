using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace App2.Utils
{
    public class RegexValidation
    {
        private RegexOptions _options;
        private TimeSpan _matchTimeout;

        public RegexValidation()
        {
            this._options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;
        }

        public static string NAME_PERSON = "^[a-zA-Z]{4,}(?: [a-zA-Z]+){0,2}$";
        public static string MOBILE_NUMBER = @"^\d{9}$";
        public static string DNI = @"^\d{8}$";
        public static string ADDRESS = @"[A-Za-z0-9'\.\-\s\,]";

        public bool IsValid(string? input, string pattern)
        {
            if(input == null) return false;

            return Regex.IsMatch(input?.ToString(), pattern);
        }

        public bool IsValidEmail(string? email)
        {
            if(email == null) return false;

            return new EmailAddressAttribute().IsValid(email);
        }
    }
}
