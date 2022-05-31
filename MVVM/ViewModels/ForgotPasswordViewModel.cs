using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using RestSharp;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFBakeryShopAdminV2.LocalValidationRules;
using WPFBakeryShopAdminV2.MVVM.Views;
using WPFBakeryShopAdminV2.Utilities;
using LangStr = WPFBakeryShopAdminV2.Utilities.Language;

namespace WPFBakeryShopAdminV2.MVVM.ViewModels
{
    public class ForgotPasswordViewModel : Screen
    {
        private string _email;
        private readonly LoginViewModel _loginViewModel;
        private readonly IWindowManager _windowManager;

        #region Base
        public ForgotPasswordViewModel(LoginViewModel loginViewModel, IWindowManager windowManager)
        {
            _loginViewModel = loginViewModel;
            _windowManager = windowManager;
        }

        public async Task SendEmail()
        {
            if (HasErrors()) return;

            RestClient client = new RestClient(RestConnection.AUTHENTICATE_BASE_CONNECTION_STRING);
            var task = await RestConnection.ExecuteRequestAsync(client, Method.Post, "/reset-password/init", Email, "text/plain");
            int statusCode = (int)task.StatusCode;
            if (statusCode == 200)
            {
                await ShowInfoMessage(LangStr.Get("Message_InfoTitle"), LangStr.Get("FP_RequestSent"));
                await GoBackToLoginForm();
            }
            else
            {
                Debug.Assert(false);
            }
        }

        private async Task ShowInfoMessage(string title, string message)
        {
            await MessageUtils.ShowInfoMessage(View.DialogContent, View.InfoTitleTB, View.InfoMessageTB, null, null, title,
                 message, View.InfoContent);
        }

        public async Task SendEmail(ActionExecutionContext context)
        {
            KeyEventArgs keyArgs = context.EventArgs as KeyEventArgs;

            if (keyArgs != null && keyArgs.Key == Key.Enter)
            {
                await SendEmail();
            }
        }
        public async Task Cancel()
        {
            await GoBackToLoginForm();
        }
        private async Task GoBackToLoginForm()
        {
            await this._windowManager.ShowWindowAsync(_loginViewModel);
            await this.DeactivateAsync(true);
        }
        private bool HasErrors()
        {
            bool test1 = !StringUtils.IsValidEmail(View.txtEmail.Text);
            bool test2 = NotEmptyValidationRule.TryNotifyEmptyField(View.txtEmail);
            return test1 || test2;

        }
        #endregion

        #region Properties
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }
        public ForgotPasswordView View
        {
            get
            {
                return (ForgotPasswordView)this.GetView();
            }
        }
        #endregion

    }
    enum Content
    {
        SUCCESS, FAIL
    }
}
