using System.Windows;
using WPFBakeryShopAdminV2.Utilities;

namespace WPFBakeryShopAdminV2.MVVM.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }
        private void LanguageList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Utilities.Language.SwitchLanguage(LanguageList.SelectedIndex);
            _=  MessageUtils.ShowInfoMessage(DialogContent, InfoTitleTB, InfoMessageTB, null, null, "Thông báo",
                "Một số thiết lập ngôn ngữ cần phải khởi động lại chương trình để sử dụng",InfoContent);
            //Some language settings will only be applied after the program is restarted
        }
    }
}
