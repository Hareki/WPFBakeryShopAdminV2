using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using RestSharp;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFBakeryShopAdminV2.LocalValidationRules;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;
using WPFBakeryShopAdminV2.MVVM.Views;
using WPFBakeryShopAdminV2.Utilities;

namespace WPFBakeryShopAdminV2.MVVM.ViewModels
{
    public class NewProductViewModel : Screen
    {
        private RestClient _restClient;
        private BindableCollection<Category> _categoryList;
        private Category _selectedCategory;
        private ProductDetails _productDetails;
        private readonly ProductViewModel _productViewModel;
        private readonly IWindowManager _windowManager;
        private Visibility _loadingPageVis = Visibility.Hidden;

        #region Base
        public NewProductViewModel(ProductViewModel productViewModel, IWindowManager windowManager, BindableCollection<Category> categoryList)
        {
            _productViewModel = productViewModel;
            _windowManager = windowManager;
            CategoryList = categoryList;
        }
        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _restClient = RestConnection.ManagementRestClient;
            ProductDetails = new ProductDetails();
            return Task.CompletedTask;
        }
        public async Task AddNewProduct()
        {

            if (SelectedCategory == null)
            {
                await ShowErrorMessage("Lỗi tải danh mục", "Không tìm thấy danh mục nào, vui lòng thử lại sau");
                CancelAdding();
                return;
            }
            if (HasErrors())
            {
                await ShowErrorMessage("Lỗi nhập liệu", "Vui lòng nhập đầy đủ thông tin và đúng định dạng");
                return;
            }

            LoadingPageVis = Visibility.Visible;
            ProductDetails.CategoryId = SelectedCategory.Id;
            string JsonProductInfo = StringUtils.SerializeObject(ProductDetails);
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Post, "products", JsonProductInfo, "application/json");
            int statusCode = (int)response.StatusCode;
            switch (statusCode)
            {
                case 200:
                    await _productViewModel.LoadLastPage();
                    _productViewModel.FocusProductRowItem(ProductDetails.ProductRowItem);
                    ResetFields();
                    ShowSuccessMessage("Thêm sản phẩm thành công");
                    break;
                case 400:
                    await ShowErrorMessage("Xảy ra lỗi", "Tên sản phẩm đã tồn tại");
                    break;
                case 404:
                    await ShowErrorMessage("Xảy ra lỗi", "Danh mục không còn tồn tại, vui lòng khởi động lại hộp thoại này để nhận thông tin danh mục mới nhất");
                    break;
            }
            LoadingPageVis = Visibility.Hidden;
        }

        private bool HasErrors()
        {
            bool test1 = NotEmptyValidationRule.TryNotifyEmptyField(View.txtAllergens);
            bool test2 = NotEmptyValidationRule.TryNotifyEmptyField(View.txtIngredients);
            bool test3 = NotEmptyValidationRule.TryNotifyEmptyField(View.txtName);
            View.AddNewProduct.Focus();
            return test1 && test2 && test3;
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

        public Visibility LoadingPageVis
        {
            get => _loadingPageVis;
            set
            {
                _loadingPageVis = value;
                NotifyOfPropertyChange(() => LoadingPageVis);
            }
        }
        #endregion

        #region Show Messages
        private async Task ShowErrorMessage(string title, string message)
        {
            await MessageUtils.ShowErrorMessageInDialog(title, message, _windowManager);
        }
        private void ShowSuccessMessage(string message)
        {
            MessageUtils.ShowSnackBarMessage(View, View.GreenMessage, View.GreenSB, View.GreenContent, message, View.RedSB);
        }
        #endregion
    }
}
