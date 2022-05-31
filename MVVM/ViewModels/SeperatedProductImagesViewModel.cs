using Caliburn.Micro;
using Microsoft.Win32;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFBakeryShopAdminV2.MVVM.Models.Bodies;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;
using WPFBakeryShopAdminV2.MVVM.Views;
using WPFBakeryShopAdminV2.Utilities;
using LangStr = WPFBakeryShopAdminV2.Utilities.Language;

namespace WPFBakeryShopAdminV2.MVVM.ViewModels
{
    public class SeperatedProductImagesViewModel : Screen
    {
        private BindableCollection<ProductImage> _rowItemImages;
        private readonly ProductViewModel _productViewModel;
        private readonly IWindowManager _windowManager;
        private readonly RestClient _restClient;
        private string _productName;
        private string _totalImages;
        private Visibility _loadingPageVis = Visibility.Hidden;

        #region Base
        public SeperatedProductImagesViewModel(ProductViewModel productViewModel,
           string productName, int totalImages, bool isDeleteEnabled, bool isAddEnabled, IWindowManager windowManager, RestClient restClient)
        {
            _productViewModel = productViewModel;
            RowItemImages = productViewModel.RowItemImages;
            SetSelectedItemsFromInnerGrid();

            ProductName = productName;
            IsDeleteEnabled = isDeleteEnabled;
            IsAddEnabled = isAddEnabled;
            _windowManager = windowManager;
            this._restClient = restClient;
            _ = ProductName != null ? TotalImages = totalImages.ToString() : TotalImages = "_";
        }
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            View.AddImagesAsync.IsEnabled = IsAddEnabled;
            View.ConfirmDeleteImages.IsEnabled = IsDeleteEnabled;
            SetSelectedItemsFromInnerGrid();
        }
        #endregion

        #region Events
        public async Task ConfirmDeleteImages()
        {
            bool confirm = await ShowConfirmMessage(LangStr.Get("Message_ConfirmationTitle"), LangStr.Get("Product_ConfirmDeletingImage"));
            if (!confirm) return;
            await DeleteImagesAsync();

        }
        private async Task DeleteImagesAsync()
        {
            LoadingPageVis = Visibility.Visible;
            List<ProductImage> list = _productViewModel.ImagesGrid.SelectedItems.Cast<ProductImage>().ToList();

            DeleteImagesBody deleteImagesBody = new DeleteImagesBody
            {
                DeletedImageIds = (from ProductImage image in list
                                   select image.Id).ToList()
            };
            string requestBody = StringUtils.SerializeObject(deleteImagesBody);
            int productId = _productViewModel.SelectedProduct.Id;
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Delete, $"products/{productId}/images", requestBody, "application/json");

            int statusCode = (int)response.StatusCode;
            LoadingPageVis = Visibility.Hidden;
            switch (statusCode)
            {
                case 200:
                    ShowSuccessMessage("Xóa ảnh thành công");
                    await _productViewModel.LoadProductImagesAsync(productId);
                    RowItemImages = _productViewModel.RowItemImages;

                    break;
                case 404:
                    await ShowErrorMessage(LangStr.Get("Message_ErrorTitle"), LangStr.Get("Product_ImageNoExists"));
                    break;
                case 400:
                    await ShowErrorMessage(LangStr.Get("Message_ErrorTitle"), LangStr.Get("Product_ImageNoBelong"));
                    break;
            }
            
        }
        public void RowItemImages_SelectionChanged()
        {
            _productViewModel.SetSelectedItems(ImagesGrid.SelectedItems);
            View.ConfirmDeleteImages.IsEnabled = (ImagesGrid.SelectedItems.Count > 0);
        }
        private void SetSelectedItemsFromInnerGrid()
        {
            foreach (var item in _productViewModel.ImagesGrid.SelectedItems)
            {
                ImagesGrid?.SelectedItems.Add(item);
            }
        }
        public async Task AddImagesAsync()//Tương tự như hàm ở ngoài product view, chỉ sửa lại tham số và thêm 1 dòng để refresh lại outer grid
        {
            OpenFileDialog open = Constants.OpenFileDialog;
            var images = new List<KeyValuePair<string, string>>();
            if ((bool)open.ShowDialog())
            {
                LoadingPageVis = Visibility.Visible;
                foreach (string element in open.FileNames)
                {
                    images.Add(new KeyValuePair<string, string>("images", element));
                }
                int productId = _productViewModel.SelectedProduct.Id;
                var response = await RestConnection.ExecuteFileRequestAsync(_restClient, Method.Post, $"products/{productId}/images", images);
                int statusCode = (int)response.StatusCode;
                LoadingPageVis = Visibility.Hidden;
                if (statusCode == 201)
                {
                    ShowSuccessMessage(LangStr.Get("Product_ImageAdded200"));
                    await _productViewModel.LoadProductImagesAsync(productId);
                    RowItemImages = _productViewModel.RowItemImages;
                }
                else
                {
                    await ShowErrorMessage(LangStr.Get("Message_ErrorTitle"), LangStr.Get("UnexpectedError"));
                }
                
            }
        }
        #endregion

        #region Show Messages
        private Task<bool> ShowConfirmMessage(string title, string message)
        {
            return MessageUtils.ShowConfirmMessageInDialog(title, message, _windowManager);
        }
        private async Task ShowErrorMessage(string title, string message)
        {
            await MessageUtils.ShowErrorMessageInDialog(title, message, _windowManager);
        }
        private void ShowSuccessMessage(string message)
        {
            MessageUtils.ShowSnackBarMessage(View, View.GreenMessage, View.GreenSB, View.GreenContent, message);
        }
        #endregion

        public BindableCollection<ProductImage> RowItemImages
        {
            get => _rowItemImages;
            set
            {
                _rowItemImages = value;
                NotifyOfPropertyChange(() => RowItemImages);
            }
        }
        public SeperatedProductImagesView View => (SeperatedProductImagesView)this.GetView();
        public DataGrid ImagesGrid
        {
            get
            {
                if (View != null)
                {
                    return View.RowItemImages;
                }
                else return null;
            }
        }
        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                NotifyOfPropertyChange(() => ProductName);
            }
        }
        public bool IsDeleteEnabled { get; }
        public bool IsAddEnabled { get; }
        public string TotalImages
        {
            get => _totalImages; set
            {
                _totalImages = value;
                NotifyOfPropertyChange(() => TotalImages);
            }
        }

        public Visibility LoadingPageVis
        {
            get => _loadingPageVis; set
            {
                _loadingPageVis = value;
                NotifyOfPropertyChange(() => LoadingPageVis);
            }
        }
    }
}
