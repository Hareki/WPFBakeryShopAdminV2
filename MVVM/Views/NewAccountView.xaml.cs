using System.Windows;
using WPFBakeryShopAdminV2.Utilities;

namespace WPFBakeryShopAdminV2.MVVM.Views
{
    /// <summary>
    /// Interaction logic for NewAccountView.xaml
    /// </summary>
    public partial class NewAccountView : Window
    {
        public NewAccountView()
        {
            InitializeComponent();
        }

        private void PreviewPhoneInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !StringUtils.NUMERIC_REGEX.IsMatch(e.Text);
        }

        private void PreviewPhoneKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
