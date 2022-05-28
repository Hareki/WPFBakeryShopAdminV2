using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;
using WPFBakeryShopAdminV2.MVVM.Views;
using WPFBakeryShopAdminV2.Utilities;

namespace WPFBakeryShopAdminV2.MVVM.ViewModels
{
    public class NewAccountViewModel : Screen
    {
        private RestClient _restClient;
        private List<LanguageItem> _languageList;
        private List<Role> _roleList;
        private LanguageItem _selectedLanguageItem;
        private PersonalAccount _personalAccount;

        #region Base
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

            SetPersonalAccountAuths();
            string JSonAccountInfo = StringUtils.SerializeObject(PersonalAccount);
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Post, "accounts", JSonAccountInfo, "application/json");
            int statusCode = (int)response.StatusCode;
            if (statusCode == 201)
            {
                ResetFields();
                ShowSuccessMessage("Tạo tài khoản thành công, vui lòng kiểm tra email đã đăng ký");
            }
            else if (statusCode == 400)
            {
                ShowFailMessage("Tạo tài khoản thất bại, email đã được sử dụng");
            }
            else
            {
                ShowFailMessage("Xảy ra lỗi khi tạo tài khoản");
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
            return !StringUtils.IsValidEmail(PersonalAccount.Email) ||
                   !StringUtils.IsValidPhoneNumber(PersonalAccount.Phone) ||
                   string.IsNullOrEmpty(PersonalAccount.FirstName) ||
                   string.IsNullOrEmpty(PersonalAccount.LastName) ||
                   string.IsNullOrEmpty(PersonalAccount.Email) ||
                   string.IsNullOrEmpty(PersonalAccount.Phone);
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

        private void ShowFailMessage(string message)
        {
            View.Dispatcher.Invoke(() =>
            {
                View.RedMessage.Text = message;

                RedSB.MessageQueue?.Enqueue(
                View.RedContent,
                null,
                null,
                null,
                false,
                true,
                TimeSpan.FromSeconds(3));
            });
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
        #endregion

    }
}
