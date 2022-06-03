using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFBakeryShopAdminV2.LocalValidationRules;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;
using WPFBakeryShopAdminV2.MVVM.Views;
using WPFBakeryShopAdminV2.Utilities;
using LangStr = WPFBakeryShopAdminV2.Utilities.Language;

namespace WPFBakeryShopAdminV2.MVVM.ViewModels
{
    public class NewAccountViewModel : Screen
    {
        private RestClient _restClient;
        private List<LanguageItem> _languageList;
        private List<Role> _roleList;
        private LanguageItem _selectedLanguageItem;
        private PersonalAccount _personalAccount;
        private Visibility _loadingPageVis = Visibility.Hidden;
        private IWindowManager _windowManager;
        #region Base
        public NewAccountViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }
        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _restClient = RestConnection.ManagementRestClient;
            LanguageList = Lists.LanguageList.LIST;
            RoleList = Lists.RoleList.LIST;
            PersonalAccount = new PersonalAccount();
            return Task.CompletedTask;
        }
        public async Task AddNewAccount()
        {
            if (HasErrors()) return;

            PersonalAccount.LastName = StringUtils.Trim(PersonalAccount.LastName);
            PersonalAccount.FirstName = StringUtils.Trim(PersonalAccount.FirstName);
            PersonalAccount.Address = StringUtils.Trim(PersonalAccount.Address);
            PersonalAccount.Email = StringUtils.Trim(PersonalAccount.Email);
            NotifyOfPropertyChange(() => PersonalAccount);

            LoadingPageVis = Visibility.Visible;
            SetPersonalAccountAuths();
            string JSonAccountInfo = StringUtils.SerializeObject(PersonalAccount);
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Post, "accounts", JSonAccountInfo, "application/json");
            int statusCode = (int)response.StatusCode;
            LoadingPageVis = Visibility.Hidden;
            switch (statusCode)
            {
                case 201:
                    ResetFields();
                    ShowSuccessMessage(LangStr.Get("NA_EmailSent"));
                    break;
                case 400:
                    await ShowErrorMessage(LangStr.Get("Message_ErrorTitle"), LangStr.Get("NA_400"));
                    break;
                default:
                    await ShowErrorMessage(LangStr.Get("Message_ErrorTitle"), LangStr.Get("UnexpectedError"));
                    break;
            }
           
        }

        private void ResetFields()
        {
            PersonalAccount = new PersonalAccount();
            View.Dispatcher.Invoke(() =>
            {
                View.RoleList.UnselectAll();
                View.LanguageList.SelectedIndex = 0;
            });
        }

        public void CancelAdding()
        {
            this.TryCloseAsync();
        }
        private bool HasErrors()
        {
            bool test1 = !StringUtils.IsValidEmail(View.txtEmail.Text);
            bool test2 = !StringUtils.IsValidEmail(View.txtPhone.Text);
            bool test3 = NotEmptyValidationRule.TryNotifyEmptyField(View.txtFirstName);
            bool test4 = NotEmptyValidationRule.TryNotifyEmptyField(View.txtLastName);
            bool test5 = NotEmptyValidationRule.TryNotifyEmptyField(View.txtEmail); ;
            bool test6 = NotEmptyValidationRule.TryNotifyEmptyField(View.txtPhone);
            View.AddNewAccount.Focus();
            return test1 || test2 || test3 || test4 || test5 || test6;

        }

        private void SetPersonalAccountAuths()
        {
            foreach (Role item in View.RoleList.SelectedItems)
            {
                PersonalAccount.Authorities.Add(item.RoleCode.ToString());
            }
            PersonalAccount.SerializeAuth = true;
        }
        #endregion

        #region Show Messages
        private void ShowSuccessMessage(string message)
        {

            View.Dispatcher.Invoke(() =>
            {
                View.GreenMessage.Text = message;

                GreenSB.MessageQueue?.Enqueue(
                View.GreenContent,
                null,
                null,
                null,
                false,
                true,
                TimeSpan.FromSeconds(3));
            });
        }

        private async Task ShowErrorMessage(string title, string message)
        {
            await MessageUtils.ShowErrorMessageInDialog(title, message, _windowManager);
        }
        #endregion

        #region Binding Properties
        public LanguageItem SelectedItemLanguage
        {
            get
            {
                return _selectedLanguageItem;
            }
            set
            {
                _selectedLanguageItem = value;
                if (value != null)
                    PersonalAccount.LangKey = value.LangKey;
            }
        }
        public List<LanguageItem> LanguageList
        {
            get
            {
                return _languageList;
            }
            set
            {
                _languageList = value;
                NotifyOfPropertyChange(() => LanguageList);
            }
        }
        public List<Role> RoleList
        {
            get
            {
                return _roleList;
            }
            set
            {
                _roleList = value;
                NotifyOfPropertyChange(() => RoleList);
            }
        }
        public PersonalAccount PersonalAccount
        {
            get
            {
                return _personalAccount;
            }
            set
            {
                _personalAccount = value;
                NotifyOfPropertyChange(() => PersonalAccount);
            }
        }
        #endregion

        #region Mapping Properties
        public NewAccountView View
        {
            get
            { return (NewAccountView)this.GetView(); }
        }
        public Snackbar GreenSB
        {
            get { return View.GreenSB; }
        }
        public Snackbar RedSB
        {
            get { return View.RedSB; }
        }

        public Visibility LoadingPageVis
        {
            get => _loadingPageVis;
            set
            {
                _loadingPageVis = value;
                NotifyOfPropertyChange(() => LoadingPageVis);
            }
        }
        #endregion

    }
}
