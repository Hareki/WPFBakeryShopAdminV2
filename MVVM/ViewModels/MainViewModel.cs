﻿using Caliburn.Micro;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;
using WPFBakeryShopAdminV2.MVVM.Views;
using WPFBakeryShopAdminV2.Utilities;
using LangStr = WPFBakeryShopAdminV2.Utilities.Language;

namespace WPFBakeryShopAdminV2.MVVM.ViewModels
{
    public class MainViewModel : Conductor<Screen>.Collection.OneActive, IHandle<PersonalAccount>
    {
        private List<LanguageItem> _languageList;

        private readonly DashboardViewModel _dashboardViewModel;
        private readonly AccountViewModel _accountViewModel;
        private readonly BillViewModel _billViewModel;
        private readonly ProductViewModel _productViewModel;
        private readonly PersonalAccountViewModel _personalAccountViewModel;
        private readonly IEventAggregator _eventAggregator;

        private PersonalAccount _personalAccount;
        private RestClient _restClient;
        private IWindowManager _windowManager;
        private LanguageItem _selectedLanguage;

        #region Base
        public MainViewModel(DashboardViewModel dashboardViewModel, AccountViewModel accountViewModel,
            BillViewModel billViewModel, ProductViewModel productViewModel,
            PersonalAccountViewModel personalAccountViewModel, IEventAggregator eventAggregator, IWindowManager windowManager)
        {
            _dashboardViewModel = dashboardViewModel;
            _accountViewModel = accountViewModel;
            _billViewModel = billViewModel;
            _productViewModel = productViewModel;
            _personalAccountViewModel = personalAccountViewModel;

            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
            Items.AddRange(new Screen[] { _dashboardViewModel, _accountViewModel, _billViewModel, _productViewModel, _personalAccountViewModel });
        }
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            LanguageList = Lists.LanguageList.LIST;
            _restClient = RestConnection.AccountRestClient;
            _eventAggregator.SubscribeOnPublishedThread(this);
            PersonalAccount = await GetPersonalAccountFromDBAsync();
        }
        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            _eventAggregator.Unsubscribe(this);
            return Task.CompletedTask;
        }
        protected override void OnViewReady(object view)
        {
            ActivateItemAsync(_dashboardViewModel);
            View.Dispatcher.Invoke(() =>
            {
                View.LoadDashboard.IsChecked = true;
            });
        }
        public async Task<PersonalAccount> GetPersonalAccountFromDBAsync()
        {
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Get, "");
            if ((int)response.StatusCode == 200)
            {
                var personalAccount = response.Content;
                return JsonConvert.DeserializeObject<PersonalAccount>(personalAccount);
            }
            return null;
        }
        public async Task LogOut()
        {
            bool confirm = await MessageUtils.ShowConfirmMessageInDialog(LangStr.Get("Message_ConfirmationTitle"), LangStr.Get("Main_LogoutConfirmation"), _windowManager);
            if (confirm)
                RestConnection.LogOut();
        }
        public void LanguageList_SelectionChanged()
        {
            Utilities.Language.SwitchLanguage(View.LanguageList.SelectedIndex);
            MessageUtils.ShowInfoMessageInDialog(LangStr.Get("Message_InfoTitle"), LangStr.Get("Main_LanguageReminder"), _windowManager);
        }
        #endregion

        #region Loading Pages
        public void LoadDashboard()
        {
            if (ActiveItem != _dashboardViewModel)
            {
                ActivateItemAsync(_dashboardViewModel);
            }

        }
        public void LoadAccount()
        {
            if (ActiveItem != _accountViewModel)
            {
                ActivateItemAsync(_accountViewModel);
            }

        }
        public void LoadBill()
        {
            if (ActiveItem != _billViewModel)
            {
                ActivateItemAsync(_billViewModel);
            }

        }
        public void LoadProduct()
        {
            if (ActiveItem != _productViewModel)
            {
                ActivateItemAsync(_productViewModel);
            }

        }
        bool firstTime = true;
        bool selfCall = false;
        public void LoadPersonalAccount()
        {
            if (ActiveItem != _personalAccountViewModel)
            {
                ActivateItemAsync(_personalAccountViewModel);
                if (firstTime)
                {
                    _eventAggregator.PublishOnUIThreadAsync(PersonalAccount);
                    selfCall = true;
                    firstTime = false;
                }

            }
        }
        #endregion

        #region Properties
        public List<LanguageItem> LanguageList
        {
            get { return _languageList; }
            set
            {
                _languageList = value;
                NotifyOfPropertyChange(() => LanguageList);
            }
        }
        public PersonalAccount PersonalAccount
        {
            get
            { return _personalAccount; }
            set
            {
                _personalAccount = value;
                SelectedLanguage = LanguageList.First(s => s.LangKey == value.LangKey);
                Utilities.Language.SwitchLanguage(View.LanguageList.SelectedIndex);
                NotifyOfPropertyChange(() => PersonalAccount);
            }
        }
        public LanguageItem SelectedLanguage
        {
            get
            {
                return _selectedLanguage;
            }

            set
            {
                _selectedLanguage = value;
                NotifyOfPropertyChange(() => SelectedLanguage);
            }
        }
        #endregion

        #region Singleton handler
        public Task HandleAsync(PersonalAccount account, CancellationToken cancellationToken)
        {
            if (selfCall)
            {
                selfCall = false;
                return Task.CompletedTask;
            }
            PersonalAccount = account;
            return Task.CompletedTask;
        }
        #endregion

        #region Binding Properties
        public MainView View
        {
            get
            {
                return this.GetView() as MainView;
            }
        }
        #endregion

    }
}
