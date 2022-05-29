using Caliburn.Micro;
using Newtonsoft.Json;
using RestSharp;
using System;
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
    public class BillViewModel : Screen, IViewModel
    {
        private RestClient _restClient;
        private Visibility _loadingPageVis = Visibility.Hidden;
        private Visibility _loadingInfoVis = Visibility.Hidden;
        private BindableCollection<BillRowItem> _rowItemBills;
        private BindingButtonAppearance _bindingButton;
        private BillRowItem _selectedBill;
        private BillDetails _billDetails;
        private Pagination _pagination;

        #region Base
        public BillViewModel()
        {
            Pagination = new Pagination(10, this);
        }
        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _restClient = RestConnection.ManagementRestClient;
            _ = LoadPageAsync();
            return Task.CompletedTask;
        }
        #endregion

        #region Orders Management
        public async Task LoadPageAsync()
        {

            if (RowItemBills != null)
                RowItemBills.Clear();

            LoadingPageVis = Visibility.Visible;
            var list = new List<KeyValuePair<string, string>>() {
                      new KeyValuePair<string, string>("page", Pagination.CurrentPage.ToString()),
                      new KeyValuePair<string, string>("size", Pagination.PageSize.ToString()),
                };
            var response = await RestConnection.ExecuteParameterRequestAsync(_restClient, Method.Get, "orders", list);

            if ((int)response.StatusCode == 200)
            {
                var bills = response.Content;
                RowItemBills = JsonConvert.DeserializeObject<BindableCollection<BillRowItem>>(bills);
                Pagination.UpdatePaginationStatus(response.Headers);
            }
            NotifyOfPropertyChange(() => Pagination);
            LoadingPageVis = Visibility.Hidden;

        }
        private async Task LoadDetailItem(int id)
        {
            LoadingInfoVis = Visibility.Visible;
            var request = new RestRequest($"orders/{id}", Method.Get);
            var respone = await _restClient.ExecuteAsync(request);
            if ((int)respone.StatusCode == 200)
            {
                var billDetails = respone.Content;
                BillDetails = JsonConvert.DeserializeObject<BillDetails>(billDetails);
            }
            LoadingInfoVis = Visibility.Hidden;
        }
        public async Task UpdateOrderStatus()
        {

            var request = new RestRequest($"orders/{BillDetails.Id}/status/update", Method.Put);
            var respone = _restClient.ExecuteAsync(request);
            if ((int)respone.Result.StatusCode == 200)
            {
                BillDetails.StatusId++;
                GridRefresh(BillDetails.StatusId);
                BillDetails.CanCancel = false;//Hard code, do nếu muốn cái này thay đổi tự động theo logic của webservice thì phải load lại từ db
                NotifyOfPropertyChange(() => BillDetails);
                MessageUtils.ShowSnackBarMessage(View, View.GreenMessage, View.GreenSB, View.GreenContent, "Cập nhật trạng thái đơn hàng thành công");
                return;
            }
            await ShowErrorMessage("Lỗi cập nhật", "Đã xảy ra lỗi không xác định trong quá trình cập nhật trạng thái đơn hàng");
        }
        public async Task CancelOrder()
        {
            bool confirm = await ShowConfirmMessage("Xác nhận hủy đơn", "Bạn có chắc muốn hủy đơn hàng này? Quá trình này không thể đảo ngược");
            if (confirm)
            {
                var request = new RestRequest($"orders/{BillDetails.Id}/cancel", Method.Put);
                var respone = _restClient.ExecuteAsync(request);
                int statusCode = (int)respone.Result.StatusCode;
                if (statusCode == 200)
                {
                    BillDetails.StatusId = 4;
                    GridRefresh(BillDetails.StatusId);
                    BillDetails.CanCancel = false;//Hard code, do nếu muốn cái này thay đổi tự động theo logic của webservice thì phải load lại từ db
                    NotifyOfPropertyChange(() => BillDetails);
                    ShowSuccessMessage("Hủy đơn hàng thành công");

                    return;
                }
                else if (statusCode == 400)
                {
                    await ShowErrorMessage("Lỗi hủy đơn", "Không thể hủy đơn hàng ở trạng thái này");
                    return;
                }
                await ShowErrorMessage("Lỗi hủy đơn", "Đã xảy ra lỗi không xác định trong quá trình hủy đơn hàng");
            }
        }
        #endregion

        #region Updating View Methods
        private void GridRefresh(int newStatusId)
        {
            View.Dispatcher.Invoke(() =>
            {
                int selectedRow = View.RowItemBills.SelectedIndex;

                RowItemBills[selectedRow].StatusId = newStatusId;
                View.RowItemBills.Items.Refresh();
                View.RowItemBills.SelectedIndex = selectedRow;
            });
        }
        public void PreviewUpdatedStatus()
        {
            if (BillDetails.CanUpdateOrderStatus)
            {
                int previewId = BillDetails.StatusId + 1;
                SetBindingButtonAppearance(previewId);
            }
        }
        public void PreviewCancelledStatus()
        {
            if (BillDetails.CanCancel)
            {
                int previewId = 4;
                SetBindingButtonAppearance(previewId);
            }
        }
        public void ClearPreview()
        {
            SetBindingButtonAppearance(BillDetails.StatusId);
        }
        private void SetBindingButtonAppearance(int statusId)
        {
            string text, imageUrl, background;
            switch (statusId)
            {
                case 1:
                    text = "Chờ duyệt";
                    background = Utilities.Constants.PENDING_COLOR;
                    imageUrl = "/Resources/Images/pending-white.png";
                    break;
                case 2:
                    text = "Đang giao";
                    background = Utilities.Constants.SHIPPING_COLOR;
                    imageUrl = "/Resources/Images/shipping-white.png";
                    break;
                case 3:
                    text = "Đã giao";
                    background = Utilities.Constants.SHIPPED_COLOR;
                    imageUrl = "/Resources/Images/shipped-white.png";
                    break;
                case 4:
                    text = "Đã hủy";
                    background = Utilities.Constants.CANCELLED_COLOR;
                    imageUrl = "/Resources/Images/cancelled-white.png";
                    break;
                default:
                    text = imageUrl = String.Empty;
                    background = "MediumSeaGrean";
                    break;
            }
            Console.WriteLine(text);
            Console.WriteLine(imageUrl);
            Console.WriteLine(background);
            BindingButton = new BindingButtonAppearance(text, imageUrl, background);
        }
        #endregion

        #region Events
        public void RowItemBills_SelectionChanged()
        {
            View.Dispatcher.Invoke(() =>
            {
                if (View.RowItemBills.SelectedIndex < 0)
                {
                    BillDetails = null;
                }
            });
        }
        public void Expander_Expanded()
        {
            if (SelectedBill != null)
                _ = LoadDetailItem(SelectedBill.Id);
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
            MessageUtils.ShowSnackBarMessage(View, View.GreenMessage, View.GreenSB, View.GreenContent, message);
        }
        #endregion

        #region Binding Properties
        public BindingButtonAppearance BindingButton
        {
            get { return _bindingButton; }
            set
            {
                _bindingButton = value;
                NotifyOfPropertyChange(() => BindingButton);
            }
        }
        public BindableCollection<BillRowItem> RowItemBills
        {
            get
            {
                return _rowItemBills;
            }
            set
            {
                _rowItemBills = value;
                NotifyOfPropertyChange(() => RowItemBills);
            }
        }
        public BillDetails BillDetails
        {
            get { return _billDetails; }
            set
            {
                _billDetails = value;
                if (value != null)
                    SetBindingButtonAppearance(BillDetails.StatusId);
                NotifyOfPropertyChange(() => BillDetails);
            }
        }
        public BillRowItem SelectedBill
        {
            get { return _selectedBill; }
            set
            {
                _selectedBill = value;
                if (value != null && View.DetailExpander.IsExpanded)
                    _ = LoadDetailItem(value.Id);

                NotifyOfPropertyChange(() => SelectedBill);
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
        public Visibility LoadingInfoVis
        {
            get { return _loadingInfoVis; }
            set
            {
                _loadingInfoVis = value;
                NotifyOfPropertyChange(() => LoadingInfoVis);
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
        public BillView View => (BillView)this.GetView();
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
    }
    public class BindingButtonAppearance
    {
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public string BackgroundColor { get; set; }
        public BindingButtonAppearance(string text, string imageUrl, string backgroundColor)
        {
            Text = text;
            ImageUrl = imageUrl;
            BackgroundColor = backgroundColor;
        }
    }
}
