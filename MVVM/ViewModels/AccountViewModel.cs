using Caliburn.Micro;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPFBakeryShopAdminV2.Interfaces;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;
using WPFBakeryShopAdminV2.Utilities;

namespace WPFBakeryShopAdminV2.MVVM.ViewModels
{
    public class AccountViewModel : Screen, IViewModel
    {
        private RestClient _restClient = RestConnection.ManagementRestClient;
        private Visibility _loadingPageVis = Visibility.Hidden;
        private BindableCollection<AccountRowItem> _rowItemAccounts;
        private AccountRowItem _selectedAccount;
        private Pagination _pagination;

        private IWindowManager _windowManager;
        #region Base
        public AccountViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            Pagination = new Pagination(10, this);
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _restClient = RestConnection.ManagementRestClient;
            _ = LoadPageAsync();
            return Task.CompletedTask;
        }
        public async Task LoadPageAsync()
        {

            if (RowItemAccounts != null)
                RowItemAccounts.Clear();

            LoadingPageVis = Visibility.Visible;

            var list = new List<KeyValuePair<string, string>>() {
                      new KeyValuePair<string, string>("page", Pagination.CurrentPage.ToString()),
                      new KeyValuePair<string, string>("size", 10.ToString()),
                };
            var response = await RestConnection.ExecuteParameterRequestAsync(_restClient, Method.Get, "accounts", list);

            if ((int)response.StatusCode == 200)
            {
                var accounts = response.Content;
                RowItemAccounts = JsonConvert.DeserializeObject<BindableCollection<AccountRowItem>>(accounts);
                Pagination.UpdatePaginationStatus(response.Headers);
            }
            NotifyOfPropertyChange(() => Pagination);
            LoadingPageVis = Visibility.Hidden;
        }

        public void ShowAddingAccountDialog()
        {
            _windowManager.ShowDialogAsync(new NewAccountViewModel());
        }
        #endregion

        #region Pagination
        public void LoadFirstPage()
        {
            Pagination.LoadFirstPage();
        }
        public void LoadPreviousPage()
        {
            Pagination.LoadPreviousPage();
        }
        public void LoadNextPage()
        {
            Pagination.LoadNextPage();
        }
        public void LoadLastPage()
        {
            Pagination.LoadLastPage();
        }
        #endregion

        #region Binding Properties
        public AccountRowItem SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                NotifyOfPropertyChange(() => SelectedAccount);
            }
        }
        public BindableCollection<AccountRowItem> RowItemAccounts
        {
            get
            {
                return _rowItemAccounts;
            }
            set
            {
                _rowItemAccounts = value;
                NotifyOfPropertyChange(() => RowItemAccounts);
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
        public Pagination Pagination
        {
            get
            {
                return _pagination;
            }
            set
            {
                _pagination = value;
                NotifyOfPropertyChange(() => Pagination);
            }
        }
        #endregion



    }
}
