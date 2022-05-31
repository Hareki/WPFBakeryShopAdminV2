using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using WPFBakeryShopAdminV2.Utilities;

namespace WPFBakeryShopAdminV2.MVVM.Views
{
    /// <summary>
    /// Interaction logic for PersonalAccountView.xaml
    /// </summary>
    public partial class PersonalAccountView : UserControl
    {
        public PersonalAccountView()
        {
            InitializeComponent();
            CurrentPasswordRevealed.Visibility = Visibility.Hidden;
            NewPasswordRevealed.Visibility = Visibility.Hidden;
            ConfirmNewPasswordRevealed.Visibility = Visibility.Hidden;

            CurrentPasswordRevealed.Padding = new Thickness(16, 16, 40, 16);
            CurrentPasswordHidden.Padding = new Thickness(16, 16, 40, 16);

            NewPasswordRevealed.Padding = new Thickness(16, 16, 40, 16);
            NewPasswordHidden.Padding = new Thickness(16, 16, 40, 16);

            ConfirmNewPasswordRevealed.Padding = new Thickness(16, 16, 40, 16);
            ConfirmNewPasswordHidden.Padding = new Thickness(16, 16, 40, 16);
        }

        private void PreviewPhoneInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !StringUtils.NUMERIC_REGEX.IsMatch(e.Text);
        }

        private void PreviewPhoneKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Space)
                e.Handled = true;
        }

        private void ShowCurrentPassword_Checked(object sender, RoutedEventArgs e)
        {
            CurrentPasswordRevealed.Visibility = Visibility.Visible;
            CurrentPasswordHidden.Visibility = Visibility.Hidden;
            CurrentPasswordRevealed.CaretIndex = CurrentPasswordRevealed.Text.Length;

        }

        private void ShowCurrentPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            CurrentPasswordRevealed.Visibility = Visibility.Hidden;
            CurrentPasswordHidden.Visibility = Visibility.Visible;
        }

        private void ShowNewPassword_Checked(object sender, RoutedEventArgs e)
        {
            NewPasswordRevealed.Visibility = Visibility.Visible;
            NewPasswordHidden.Visibility = Visibility.Hidden;
            NewPasswordRevealed.CaretIndex = NewPasswordRevealed.Text.Length;

        }

        private void ShowNewPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            NewPasswordRevealed.Visibility = Visibility.Hidden;
            NewPasswordHidden.Visibility = Visibility.Visible;
        }

        private void ShowConfirmNewPassword_Checked(object sender, RoutedEventArgs e)
        {
            ConfirmNewPasswordRevealed.Visibility = Visibility.Visible;
            ConfirmNewPasswordHidden.Visibility = Visibility.Hidden;
            ConfirmNewPasswordRevealed.CaretIndex = ConfirmNewPasswordRevealed.Text.Length;

        }

        private void ShowConfirmNewPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            ConfirmNewPasswordRevealed.Visibility = Visibility.Hidden;
            ConfirmNewPasswordHidden.Visibility = Visibility.Visible;
        }
    }
}
