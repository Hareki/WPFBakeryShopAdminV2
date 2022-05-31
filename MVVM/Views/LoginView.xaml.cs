using MaterialDesignThemes.Wpf;
using System.Windows;

namespace WPFBakeryShopAdminV2.MVVM.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            PasswordRevealed.Visibility = Visibility.Hidden;
            PasswordHidden.Padding = new Thickness(16, 16, 40, 16);
            PasswordHidden.Padding = new Thickness(16, 16, 40, 16);
        }
        private void LanguageList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Utilities.Language.SwitchLanguage(LanguageList.SelectedIndex);
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            PasswordRevealed.Visibility = Visibility.Visible;
            PasswordHidden.Visibility = Visibility.Hidden;

            PasswordRevealed.CaretIndex = PasswordRevealed.Text.Length;

        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordRevealed.Visibility = Visibility.Hidden;
            PasswordHidden.Visibility = Visibility.Visible;
        }
    }
}
