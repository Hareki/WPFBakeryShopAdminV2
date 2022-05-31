using System.Globalization;
using System.Windows.Controls;
using LangStr = WPFBakeryShopAdminV2.Utilities.Language;

namespace WPFBakeryShopAdminV2.LocalValidationRules
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public string Message { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return new ValidationResult(false, Message ?? LangStr.Get("Validation_RequiredField"));
            }
            return ValidationResult.ValidResult;
        }
        public static bool TryNotifyEmptyField(TextBox field)
        {
            if (string.IsNullOrEmpty(field.Text))
            {
                field.Text = string.Empty;
                field.Focus();
                return true;
            }
            return false;
        }
    }
}
