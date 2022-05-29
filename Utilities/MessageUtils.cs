using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFBakeryShopAdminV2.MVVM.ViewModels;
using static WPFBakeryShopAdminV2.MVVM.ViewModels.DialogViewModel;

namespace WPFBakeryShopAdminV2.Utilities
{
    public class MessageUtils
    {
        public static void ShowSnackBarMessage(UserControl view, TextBlock greenMessage, Snackbar greenSB, StackPanel greenContent, string message, Snackbar otherSB = null)
        {
            view.Dispatcher.Invoke(() =>
            {
                greenMessage.Text = message;

                ClearSnackbar(greenSB);
                if (otherSB != null)
                    ClearSnackbar(otherSB);
                greenSB.MessageQueue?.Enqueue(
                greenContent,
               null,
                null,
               null,
                false,
                true,
                TimeSpan.FromSeconds(3));
            });
        }
        private static void ClearSnackbar(Snackbar greenSB)
        {
            greenSB.MessageQueue?.Clear();
        }
        public static void ShowSnackBarMessage(Window view, TextBlock greenMessage, Snackbar greenSB, StackPanel greenContent, string message, Snackbar otherSB = null)
        {
            view.Dispatcher.Invoke(() =>
            {
                greenMessage.Text = message;

                ClearSnackbar(greenSB);
                if (otherSB != null)
                    ClearSnackbar(otherSB);
                greenSB.MessageQueue?.Enqueue(
                greenContent,
               null,
                null,
               null,
                false,
                true,
                TimeSpan.FromSeconds(3));
            });
        }
        public static async Task<bool> ShowConfirmMessage(Border dialogContent, TextBlock titleTB, TextBlock messageTB, StackPanel confirmContent
            , StackPanel errorContent, string title, string message)
        {
            confirmContent.Visibility = Visibility.Visible;
            errorContent.Visibility = Visibility.Collapsed;

            titleTB.Text = title;
            messageTB.Text = message;
            var result = await DialogHost.Show(dialogContent);
            return System.Convert.ToBoolean(result);
        }
        public static async Task ShowErrorMessage(Border dialogContent, TextBlock titleTB, TextBlock messageTB, StackPanel confirmContent
            , StackPanel errorContent, string title, string message)
        {
            confirmContent.Visibility = Visibility.Collapsed;
            errorContent.Visibility = Visibility.Visible;
            titleTB.Text = title;
            messageTB.Text = message;
            await DialogHost.Show(dialogContent);
        }

        public static async Task<bool> ShowConfirmMessageInDialog(string title, string message, IWindowManager windowManager)
        {
            bool confirm = (bool)await windowManager.ShowDialogAsync(new DialogViewModel(title, message, ContentType.Confirm));
            return confirm;
        }
        public static Task ShowErrorMessageInDialog(string title, string message, IWindowManager windowManager)
        {
            return windowManager.ShowDialogAsync(new DialogViewModel(title, message, ContentType.Error));

        }
    }
}
