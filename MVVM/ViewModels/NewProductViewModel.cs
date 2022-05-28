using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;
using WPFBakeryShopAdminV2.MVVM.Models.Bodies;
using WPFBakeryShopAdminV2.MVVM.Views;
using WPFBakeryShopAdminV2.Utilities;
using WPFBakeryShopAdminV2.MVVM.ViewModels;

namespace WPFBakeryShopAdminV2.MVVM.ViewModels
{
    public class NewProductViewModel : Screen
    {
        private RestClient _restClient;
        private BindableCollection<Category> _categoryList;
        private Category _selectedCategory;
        private ProductDetails _productDetails;
        #region Base
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _restClient = RestConnection.ManagementRestClient;
            ProductDetails = new ProductDetails();
            await LoadCategoryList();

        }
        private async Task LoadCategoryList()
        {
            var categoryList = Lists.CategoryList.LoadCategoryList();
            CategoryList = new BindableCollection<Category>((await categoryList));
            if (CategoryList != null && CategoryList.Count > 0)
            {
                ProductDetails.CategoryId = CategoryList[0].Id;
                SelectedCategory = CategoryList[0];
            }
        }

        public async Task AddNewProduct()
        {
            if (SelectedCategory == null)
            {
                await DialogHost.Show(View.DialogContent);
                CancelAdding();
                return;
            }
            if (ProductInfoHasErrors()) return;

            string JSonAccountInfo = StringUtils.SerializeObject(ProductDetails);
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Post, "products", JSonAccountInfo, "application/json");
            int statusCode = (int)response.StatusCode;
            if (statusCode == 201)
            {
                ResetFields();
                ShowSuccessMessage("Thêm sản phẩm thành công");
            }
            else if (statusCode == 400)
            {
                await ShowErrorMessage("Xảy ra lỗi", "Tên sản phẩm đã tồn tại");
            }
            else if (statusCode == 404)
            {
                await ShowErrorMessage("Xảy ra lỗi", "Danh mục không còn tồn tại, vui lòng khởi động lại hộp thoại này để nhận thông tin danh mục mới nhất");
            }
        }

        private bool ProductInfoHasErrors()
        {
            return string.IsNullOrEmpty(ProductDetails.Name);
        }

        private void ResetFields()
        {
            ProductDetails = new ProductDetails();
            View.Dispatcher.Invoke(() =>
            {
                View.CategoryList.SelectedIndex = 0;
            });

        }

        public void CancelAdding()
        {
            this.TryCloseAsync();
        }
        #endregion

        #region Binding Property
        public BindableCollection<Category> CategoryList
        {
            get => _categoryList;
            set
            {
                _categoryList = value;
                NotifyOfPropertyChange(() => CategoryList);
            }
        }
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                NotifyOfPropertyChange(() => SelectedCategory);
            }
        }
        public ProductDetails ProductDetails
        {
            get { return _productDetails; }
            set
            {
                _productDetails = value;
                if (value != null)
                {
                    SelectedCategory = value.Category;
                }
                NotifyOfPropertyChange(() => ProductDetails);
            }
        }
        public NewProductView View => (NewProductView)this.GetView();
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
        #endregion
    }
}
