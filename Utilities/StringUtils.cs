﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WPFBakeryShopAdminV2.Utilities
{
    public class StringUtils
    {
        private static readonly Regex _emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        private static readonly Regex _phoneRegex = new Regex(@"^0[0-9]{9}$");
        public static readonly Regex NUMERIC_REGEX = new Regex(@"^[0-9]*$");
        public static bool IsValidEmail(string email)
        {
            if (email == null) return false;
            return _emailRegex.IsMatch(email);
        }
        public static bool IsValidPhoneNumber(string phoneNum)
        {
            if (phoneNum == null) return false;
            return _phoneRegex.IsMatch(phoneNum);
        }
        public static string FormatCurrency(int currency)
        {
            return string.Format(new CultureInfo("vi-VN"), "{0:C0}", currency);
        }
        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
