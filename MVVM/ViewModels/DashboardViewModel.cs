using Caliburn.Micro;
using Newtonsoft.Json;
using RestSharp;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;
using WPFBakeryShopAdminV2.Utilities;

namespace WPFBakeryShopAdminV2.MVVM.ViewModels
{
    public class DashboardViewModel : Screen
    {
        private RestClient _restClient;
        private Visibility _loadingPageVis = Visibility.Visible;
        private Dashboard _dashboard;
        private float _cancelledPercent;
        private float _shippedPercent;
        private float _pendingPercent;
        private float _shippingPercent;

        #region Base
        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _restClient = RestConnection.ManagementRestClient;
            LoadPage();
            return Task.CompletedTask;
        }
        private void UpdatePercent()
        {
            float todayOrdersNum = Dashboard.TodayOrdersNum;
            CancelledPercent = ((float)Dashboard.TodayCancelOrdersNum / todayOrdersNum) * 100;
            ShippingPercent = ((float)Dashboard.TodayDispatchOrdersNum / todayOrdersNum) * 100;
            ShippedPercent = ((float)Dashboard.TodayShippedOrdersNum / todayOrdersNum) * 100;
            PendingPercent = ((float)Dashboard.TodayProcessingOrdersNum / todayOrdersNum) * 100;
        }
        public void LoadPage()
        {
            new Thread(new ThreadStart(() =>
            {
                LoadingPageVis = Visibility.Visible;
                var response = RestConnection.ExecuteRequestAsync(_restClient, Method.Get, "home");
                if ((int)response.Result.StatusCode == 200)
                {
                    var dashboardContent = response.Result.Content;
                    Dashboard = JsonConvert.DeserializeObject<Dashboard>(dashboardContent);
                    UpdatePercent();
                }
                LoadingPageVis = Visibility.Hidden;
            })).Start();

        }
        #endregion

        #region Binding Properties
        public float CancelledPercent
        {
            get { return _cancelledPercent; }
            set
            {
                _cancelledPercent = value;
                NotifyOfPropertyChange(() => CancelledPercent);
            }
        }
        public float PendingPercent
        {
            get { return _pendingPercent; }
            set
            {
                _pendingPercent = value;
                NotifyOfPropertyChange(() => PendingPercent);
            }
        }
        public float ShippingPercent
        {
            get { return _shippingPercent; }
            set
            {
                _shippingPercent = value;
                NotifyOfPropertyChange(() => ShippingPercent);
            }
        }
        public float ShippedPercent
        {
            get { return _shippedPercent; }
            set
            {
                _shippedPercent = value;
                NotifyOfPropertyChange(() => ShippedPercent);
            }
        }
        public Dashboard Dashboard
        {
            get { return _dashboard; }
            set
            {
                _dashboard = value;
                NotifyOfPropertyChange(() => Dashboard);
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
        #endregion

    }
}
