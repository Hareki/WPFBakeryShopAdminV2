using System.Globalization;
using System.Windows.Controls;

namespace WPFBakeryShopAdminV2.LocalValidationRules
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public string Message { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return new ValidationResult(false, Message ?? "A value is required");
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
