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
using LangStr = WPFBakeryShopAdminV2.Utilities.Language;

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
        private Visibility _updatingStatusVis = Visibility.Hidden;
        private bool _activated = false;

        #region Base
        public BillViewModel()
        {
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
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Get, $"orders/{id}");
            if ((int)response.StatusCode == 200)
            {
                var billDetails = response.Content;
                BillDetails = JsonConvert.DeserializeObject<BillDetails>(billDetails);
            }
            LoadingInfoVis = Visibility.Hidden;
        }
        public async Task UpdateOrderStatus()
        {
            UpdatingStatusVis = Visibility.Visible;
            var respone = await RestConnection.ExecuteRequestAsync(_restClient, Method.Put, $"orders/{BillDetails.Id}/status/update");
            UpdatingStatusVis = Visibility.Hidden;
            if ((int)respone.StatusCode == 200)
            {
                BillDetails.StatusId++;
                GridRefresh(BillDetails.StatusId);
                BillDetails.CanCancel = false;//Hard code, do nếu muốn cái này thay đổi tự động theo logic của webservice thì phải load lại từ db
                NotifyOfPropertyChange(() => BillDetails); ////Notify các trạng thái enable của 2 nút cập nhật
                SetBindingButtonAppearance(BillDetails.StatusId);
                ShowSuccessMessage(LangStr.Get("Order_UpdateStatus200"));
            }
            else
            {
                await ShowErrorMessage(LangStr.Get("Message_ErrorTitle"), LangStr.Get("UnexpectedError"));
            }
            

        }
        public async Task CancelOrder()
        {

            bool confirm = await ShowConfirmMessage(LangStr.Get("Message_ConfirmationTitle"), LangStr.Get("Order_ConfirmCancelling"));
            if (confirm)
            {
                UpdatingStatusVis = Visibility.Visible;
                var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Put, $"orders/{BillDetails.Id}/cancel");
                int statusCode = (int)response.StatusCode;
                UpdatingStatusVis = Visibility.Hidden;
                switch (statusCode)
                {
                    case 200:
                        BillDetails.StatusId = 4;
                        GridRefresh(BillDetails.StatusId);
                        BillDetails.CanCancel = false;//Hard code, do nếu muốn cái này thay đổi tự động theo logic của webservice thì phải load lại từ db
                        NotifyOfPropertyChange(() => BillDetails); //Notify các trạng thái enable của 2 nút cập nhật
                        SetBindingButtonAppearance(BillDetails.StatusId);
                        ShowSuccessMessage(LangStr.Get("Order_Cancel200"));
                        break;
                    case 400:
                        await ShowErrorMessage(LangStr.Get("Message_ErrorTitle"), LangStr.Get("Order_Cancel400"));
                        break;
                    default:
                        await ShowErrorMessage(LangStr.Get("Message_ErrorTitle"), LangStr.Get("UnexpectedError"));
                        break;
                }
                
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
               // View.RowItemBills.SelectedIndex = selectedRow; tương tự như giải thích ở product view model
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
                    text = LangStr.Get("Pending");
                    background = Utilities.Constants.PENDING_COLOR;
                    imageUrl = "/Resources/Images/pending-white.png";
                    break;
                case 2:
                    text = LangStr.Get("Shipping");
                    background = Utilities.Constants.SHIPPING_COLOR;
                    imageUrl = "/Resources/Images/shipping-white.png";
                    break;
                case 3:
                    text = LangStr.Get("Shipped");
                    background = Utilities.Constants.SHIPPED_COLOR;
                    imageUrl = "/Resources/Images/shipped-white.png";
                    break;
                case 4:
                    text = LangStr.Get("Cancelled");
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
        public async void Expander_Expanded()
        {
            if (SelectedBill != null)
                _ = LoadDetailItem(SelectedBill.Id);
            await Task.Delay(300);

            var selectedItem = View.RowItemBills.SelectedItem;
            if (selectedItem != null)
                View.RowItemBills.ScrollIntoView((selectedItem));
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
        public Visibility UpdatingStatusVis
        {
            get => _updatingStatusVis;
            set
            {
                _updatingStatusVis = value;
                NotifyOfPropertyChange(() => UpdatingStatusVis);
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
