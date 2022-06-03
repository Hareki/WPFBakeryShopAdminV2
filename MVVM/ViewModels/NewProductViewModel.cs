using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFBakeryShopAdminV2.LocalValidationRules;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;
using WPFBakeryShopAdminV2.MVVM.Views;
using WPFBakeryShopAdminV2.Utilities;
using LangStr = WPFBakeryShopAdminV2.Utilities.Language;
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
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _restClient = RestConnection.ManagementRestClient;
            ProductDetails = new ProductDetails();

            if (CategoryList.Count > 0)
                SelectedCategory = CategoryList[0];
            else
            {
                await ShowErrorMessage(LangStr.Get("Message_ErrorTitle"), "Không tìm thấy danh mục nào, vui lòng thử lại sau");
                CancelAdding();
            }
        }
        public async Task AddNewProduct()
        {

            if (HasErrors()) return;

            LoadingPageVis = Visibility.Visible;

            ProductDetails.Name = StringUtils.Trim(ProductDetails.Name);
            ProductDetails.Allergens = StringUtils.Trim(ProductDetails.Allergens);
            ProductDetails.Ingredients = StringUtils.Trim(ProductDetails.Ingredients);
            NotifyOfPropertyChange(() => ProductDetails);

            ProductDetails.CategoryId = SelectedCategory.Id;
            string JsonProductInfo = StringUtils.SerializeObject(ProductDetails);
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Post, "products", JsonProductInfo, "application/json");
            int statusCode = (int)response.StatusCode;
            LoadingPageVis = Visibility.Hidden;
            switch (statusCode)
            {
                case 200:
                    await _productViewModel.LoadLastPage();
                    _productViewModel.FocusProductRowItem(ProductDetails.ProductRowItem);
                    ResetFields();
                    ShowSuccessMessage(LangStr.Get("NP_200"));
                    break;
                case 400:
                    await ShowErrorMessage(LangStr.Get("Message_ErrorTitle"), LangStr.Get("NP_400"));
                    break;
                case 404:
                    await ShowErrorMessage(LangStr.Get("Message_ErrorTitle"), LangStr.Get("NP_404"));
                    break;
            }
            
        }

        private bool HasErrors()
        {
            bool test2 = NotEmptyValidationRule.TryNotifyEmptyField(View.txtIngredients);
            bool test3 = NotEmptyValidationRule.TryNotifyEmptyField(View.txtName);
            View.AddNewProduct.Focus();
            return test2 || test3;
        }

        private void ResetFields()
        {
            ProductDetails = new ProductDetails();
            try
            {
                View.Dispatcher.Invoke(() =>
                {
                    View.CategoryList.SelectedIndex = 0;
                });
            }
            catch (NullReferenceException) { }
           

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
