using System.Globalization;
using System.Windows.Controls;
using WPFBakeryShopAdminV2.Utilities;
using LangStr = WPFBakeryShopAdminV2.Utilities.Language;

namespace WPFBakeryShopAdminV2.LocalValidationRules
{
    public class PhoneFormatValidationRule : ValidationRule
    {
        public string Message { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (StringUtils.IsValidPhoneNumber(value?.ToString()))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, Message ?? LangStr.Get("Validation_PhoneFormat"));
        }
    }
}
