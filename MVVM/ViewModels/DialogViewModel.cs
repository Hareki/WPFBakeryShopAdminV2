using Caliburn.Micro;
using System.Windows;
using WPFBakeryShopAdminV2.MVVM.Views;

namespace WPFBakeryShopAdminV2.MVVM.ViewModels
{
    public class DialogViewModel : Screen
    {
        private string _title;
        private string _message;
        private ContentType _contentType;
        public DialogViewModel(string title, string message, ContentType contentType)
        {
            _title = title;
            _message = message;
            _contentType = contentType;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            if (_contentType == ContentType.Confirm)
            {
                View.ConfirmContent.Visibility = Visibility.Visible;
                View.ErrorContent.Visibility = Visibility.Collapsed;

                View.ConfirmTitleTB.Text = _title;
                View.ConfirmMessageTB.Text = _message;
            }
            else
            {
                View.ConfirmContent.Visibility = Visibility.Collapsed;
                View.ErrorContent.Visibility = Visibility.Visible;

                View.ErrorTitleTB.Text = _title;
                View.ErrorMessageTB.Text = _message;
            }
        }

        public void ButtonOkayClicked()
        {
            this.TryCloseAsync(true);
        }
        public void ButtonConfirmClicked()
        {
            this.TryCloseAsync(true);
        }
        public void ButtonCancelClicked()
        {
            this.TryCloseAsync(false);
        }

        public enum ContentType
        {
            Confirm, Error
        }
        private DialogView View => (DialogView)this.GetView();
    }
}
