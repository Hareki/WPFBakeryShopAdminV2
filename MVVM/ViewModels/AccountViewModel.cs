using Caliburn.Micro;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPFBakeryShopAdminV2.Interfaces;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;
using WPFBakeryShopAdminV2.MVVM.Views;
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
        private bool _activated = false;
        #region Base
        public AccountViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            Pagination = new Pagination(10, this);
        }
        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            if (!_activated)
            {
                _restClient = RestConnection.ManagementRestClient;
                _ = LoadPageAsync();
                _activated = true;
            }
            return Task.CompletedTask;
        }
        #endregion

        #region Loading Stuffs
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
        #endregion

        #region Account Management
        public void ShowAddingAccountDialog()
        {
            _windowManager.ShowDialogAsync(new NewAccountViewModel(_windowManager));
        }
        public async void Expander_Expanded()
        {
            await Task.Delay(300);

            var selectedItem = View.RowItemAccounts.SelectedItem;
            if (selectedItem != null)
                View.RowItemAccounts.ScrollIntoView((selectedItem));
        }
        #endregion

        #region Pagination
        public async Task LoadFirstPage()
        {
            await Pagination.LoadFirstPage();
        }
        public async Task LoadPreviousPage()
        {
            await Pagination.LoadPreviousPage();
        }
        public async Task LoadNextPage()
        {
            await Pagination.LoadNextPage();
        }
        public async Task LoadLastPage()
        {
            await Pagination.LoadLastPage();
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
        public AccountView View => (AccountView)this.GetView();
        #endregion



    }
}
