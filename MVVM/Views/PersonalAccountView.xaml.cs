using System.Text.RegularExpressions;
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
