using Caliburn.Micro;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WPFBakeryShopAdminV2.MVVM.Models.Bodies;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;
using WPFBakeryShopAdminV2.Utilities;
using WPFBakeryShopAdminV2.MVVM.Views;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System;

namespace WPFBakeryShopAdminV2.MVVM.ViewModels
{
    public class PersonalAccountViewModel : Screen
    {
        private RestClient _restClient;
        private Visibility _loadingPageVis = Visibility.Hidden;
        private PersonalAccount _personalAccount;
        private PersonalAccount _accountBeforeEditing = null;
        private List<LanguageItem> _languageList;
        private bool _editing = false;
        private string _userImageUrl;
        private LanguageItem _selectedItemLanguage;
        private ChangePasswordBody _changePasswordBody;
        private IEventAggregator _eventAggregator;

        #region Base
        public PersonalAccountViewModel(IEventAggregator eventAggregator)
        {
            ChangePasswordBody = new ChangePasswordBody();
            _eventAggregator = eventAggregator;
        }
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _restClient = RestConnection.AccountRestClient;
            _eventAggregator.SubscribeOnPublishedThread(this);
            LanguageList = Lists.LanguageList.LIST;

            if (PersonalAccount == null)
            {
                await RefreshAccountInfo();
            }
        }
        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            base.OnDeactivateAsync(close, cancellationToken);
            _eventAggregator.Unsubscribe(this);
            return Task.CompletedTask;
        }
        public async Task<PersonalAccount> GetPersonalAccountFromDBAsync()
        {
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Get, "", null, null);
            if ((int)response.StatusCode == 200)
            {
                var personalAccount = response.Content;
                return JsonConvert.DeserializeObject<PersonalAccount>(personalAccount);
            }
            return null;
        }
        public void RequestUpdate()
        {
            Editing = true;
            _accountBeforeEditing = new PersonalAccount(PersonalAccount);
        }
        public void CancelUpdate()
        {
            Editing = false;
            PersonalAccount = new PersonalAccount(_accountBeforeEditing);
        }
        public async Task UpdatePreviewImage()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            open.Multiselect = false;
            if ((bool)open.ShowDialog())
            {
                FileInfo fi = new FileInfo(open.FileName);
                float fileSizeInMb = (float)fi.Length / 1000000;
                if (fileSizeInMb <= 1)
                {
                    UserImageUrl = open.FileName;
                }
                else
                {
                    await ShowErrorMessage("Lỗi cập nhật ảnh", "Vui lòng chọn file có kích thước nhỏ hơn");
                }
            }
        }
        public void ReloadPage()
        {
            _ = RefreshAccountInfo();
        }
        private async Task RefreshAccountInfo()
        {
            LoadingPageVis = Visibility.Visible;
            PersonalAccount temp = await GetPersonalAccountFromDBAsync();
            await _eventAggregator.PublishOnUIThreadAsync(temp);

            PersonalAccount = temp;
            LoadingPageVis = Visibility.Hidden;
        }
        private async Task<bool> UpdateAccountInfoAsync()
        {
            string JSonAccountInfo = StringUtils.SerializeObject(PersonalAccount);

            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Put, "info", JSonAccountInfo, "application/json");
            int statusCode = (int)response.StatusCode;
            if (statusCode == 200)
            {
                return true;
            }
            else if (statusCode == 400)
            {
                await ShowErrorMessage("Lỗi cập nhật thông tin", "Địa chỉ email đã được sử dụng");
                return false;
            }
            return false;
        }
        private async Task<bool> UpdateAccountImageAsync()
        {
            if (!UserImageUrl.Contains("http"))
            {
                var images = new List<KeyValuePair<string, string>>() {
                      new KeyValuePair<string, string>("image", UserImageUrl)
                };
                var response = await RestConnection.ExecuteFileRequestAsync(_restClient, Method.Put, "image", images);
                int statusCode = (int)response.StatusCode;
                if (statusCode == 200)
                {
                    return true;
                }
                else if (statusCode == 400)
                {
                    await ShowErrorMessage("Lỗi cập nhật ảnh", "Cập nhật ảnh thất bại");
                    return false;
                }
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task UpdatePersonalAccountAsync()
        {
            if (UpdateInfoHasErrors()) return;

            LoadingPageVis = Visibility.Visible;
            var task1 = UpdateAccountInfoAsync();
            var task2 = UpdateAccountImageAsync();

            var result1 = await task1;
            var result2 = await task2;
            if (result1 == false || result2 == false)
            {
                PersonalAccount temp = await GetPersonalAccountFromDBAsync();
                await _eventAggregator.PublishOnUIThreadAsync(temp);
                PersonalAccount = temp;
            }
            else
            {
                await _eventAggregator.PublishOnUIThreadAsync(PersonalAccount);
                ShowSuccessMessage("Cập nhật thông tin thành công");
            }

            Editing = false;
            LoadingPageVis = Visibility.Hidden;

        }
        public async Task ChangePassword()
        {
            string errorMessage = GetPasswordErrorMessage();
            if (!string.IsNullOrEmpty(errorMessage))
            {
                await ShowErrorMessage("Lỗi đổi mật khẩu", errorMessage);
                return;
            }

            string JsonChangePasswordInfo = StringUtils.SerializeObject(ChangePasswordBody);
            var response = RestConnection.ExecuteRequestAsync(_restClient, Method.Post, "change-password", JsonChangePasswordInfo, "application/json").Result;
            int statusCode = (int)response.StatusCode;
            if (statusCode == 200)
            {
                ClearPasswordBox();
                await ShowLogoutMessage("Đổi mật khẩu thành công, chuẩn bị đăng xuất...");
                RestConnection.LogOut();
            }
            else if (statusCode == 400)
            {
                await ShowErrorMessage("Lỗi đổi mật khẩu", "Mật khẩu hiện tại không đúng");
            }
        }
        private void ClearPasswordBox()
        {
            ChangePasswordBody = new ChangePasswordBody();
        }
        private string GetPasswordErrorMessage()
        {
            if (string.IsNullOrEmpty(ChangePasswordBody.CurrentPassword))
                return "Mật hiện tại không được để trống";
            if (string.IsNullOrEmpty(ChangePasswordBody.NewPassword))
                return "Mật mới không được để trống";
            if (string.IsNullOrEmpty(ChangePasswordBody.ConfirmNewPassword))
                return "Xác nhận khẩu mới không được để trống";
            if (ChangePasswordBody.NewPassword.Length < 4)
                return "Mật khẩu mới phải tối thiểu 4 ký tự";
            if (!ChangePasswordBody.NewPassword.Equals(ChangePasswordBody.ConfirmNewPassword))
            {
                return "Xác nhận mật khẩu mới không khớp";
            }
            else
            {
                return string.Empty;
            }
        }
        private bool UpdateInfoHasErrors()
        {
            return !StringUtils.IsValidEmail(PersonalAccount.Email) ||
                   !StringUtils.IsValidPhoneNumber(PersonalAccount.Phone) ||
                   string.IsNullOrEmpty(PersonalAccount.FirstName) ||
                   string.IsNullOrEmpty(PersonalAccount.LastName) ||
                   string.IsNullOrEmpty(PersonalAccount.Email) ||
                   string.IsNullOrEmpty(PersonalAccount.Phone);
        }
        #endregion

        #region Show Messages
        private async Task ShowErrorMessage(string title, string message)
        {
            await MessageUtils.ShowErrorMessage(View.DialogContent, View.ErrorTitleTB, View.ErrorMessageTB,
                   View.ConfirmContent, View.ErrorContent, title, message);
        }
        private async Task<bool> ShowConfirmMessage(string title, string message)
        {
            return await MessageUtils.ShowConfirmMessage(View.DialogContent, View.ConfirmTitleTB, View.ConfirmMessageTB, View.ConfirmContent, View.ErrorContent,
               title, message);
        }
        private void ShowSuccessMessage(string message)
        {
            MessageUtils.ShowSuccessMessage(View, View.GreenMessage, View.GreenSB, View.GreenContent, message);
        }
        private async Task ShowLogoutMessage(string message)
        {

            View.Dispatcher.Invoke(() =>
            {
                View.GreenMessage.Text = message;

                View.GreenSB.MessageQueue?.Enqueue(
                View.GreenContent,
                null,
                null,
                null,
                false,
                true,
                TimeSpan.FromSeconds(3));
            });
            await Task.Delay(3000);
        }
        #endregion

        #region Binding Properties
        public PersonalAccountView View
        {
            get
            { return (PersonalAccountView)this.GetView(); }
        }
        public PersonalAccount PersonalAccount
        {
            get { return _personalAccount; }
            set
            {
                _personalAccount = value;
                if (value != null)
                    UserImageUrl = value.ImageUrl;

                NotifyOfPropertyChange(() => PersonalAccount);
            }
        }
        public Visibility LoadingPageVis
        {
            get { return _loadingPageVis; }
            set
            {
                _loadingPageVis = value;
                NotifyOfPropertyChange(() => LoadingPageVis);
            }

        }
        public List<LanguageItem> LanguageList
        {
            get { return _languageList; }
            set
            {
                _languageList = value;
                NotifyOfPropertyChange(() => LanguageList);
            }
        }
        public bool Editing
        {
            get { return _editing; }
            set
            {
                _editing = value;
                NotifyOfPropertyChange(() => Editing);
                NotifyOfPropertyChange(() => NotEditing);
            }
        }
        public string UserImageUrl
        {
            get { return _userImageUrl; }
            set
            {
                _userImageUrl = value;
                NotifyOfPropertyChange(() => UserImageUrl);
            }
        }
        public LanguageItem SelectedItemLanguage
        {
            get
            {
                return _selectedItemLanguage;
            }
            set
            {
                _selectedItemLanguage = value;
                if (value != null)
                    PersonalAccount.LangKey = value.LangKey;
            }
        }
        public bool NotEditing
        {
            get { return !Editing; }
        }
        public ChangePasswordBody ChangePasswordBody
        {
            get { return _changePasswordBody; }
            set
            {
                _changePasswordBody = value;
                NotifyOfPropertyChange(() => ChangePasswordBody);
            }
        }
        #endregion

    }
}
