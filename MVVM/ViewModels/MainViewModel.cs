using Caliburn.Micro;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;
using WPFBakeryShopAdminV2.MVVM.Views;
using WPFBakeryShopAdminV2.Utilities;

namespace WPFBakeryShopAdminV2.MVVM.ViewModels
{
    public class MainViewModel : Conductor<Screen>.Collection.OneActive, IHandle<PersonalAccount>
    {
        private List<LanguageItem> _languageList;

        private DashboardViewModel _dashboardViewModel;
        private AccountViewModel _accountViewModel;
        private BillViewModel _billViewModel;
        private ProductViewModel _productViewModel;
        private PersonalAccountViewModel _personalAccountViewModel;
        private IEventAggregator _eventAggregator;

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
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Get, "", null, null);
            if ((int)response.StatusCode == 200)
            {
                var personalAccount = response.Content;
                return JsonConvert.DeserializeObject<PersonalAccount>(personalAccount);
            }
            return null;
        }
        public void LogOut()
        {
            RestConnection.LogOut();
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
        public void LoadPersonalAccount()
        {
            if (ActiveItem != _personalAccountViewModel)
            {
                ActivateItemAsync(_personalAccountViewModel);
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
