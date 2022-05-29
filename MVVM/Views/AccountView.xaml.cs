using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WPFBakeryShopAdminV2.MVVM.Views
{
    /// <summary>
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        public AccountView()
        {
            InitializeComponent();
        }
        private void RowItemAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PBImage.Visibility = Visibility.Visible;
            Console.WriteLine("Vis");
        }

        private void Image_Changed(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => HideLoading()), DispatcherPriority.ContextIdle, null);
        }

        private void HideLoading()
        {
            PBImage.Visibility = Visibility.Hidden;
            Console.WriteLine("Hid");
        }
    }
}
