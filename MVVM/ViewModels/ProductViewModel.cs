using Caliburn.Micro;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WPFBakeryShopAdminV2.MVVM.Models.Bodies;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;
using WPFBakeryShopAdminV2.Utilities;
using WPFBakeryShopAdminV2.MVVM.Views;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using WPFBakeryShopAdminV2.Interfaces;

namespace WPFBakeryShopAdminV2.MVVM.ViewModels
{
    public class ProductViewModel : Screen, IViewModel
    {
        private RestClient _restClient = RestConnection.ManagementRestClient;
        private Visibility _loadingPageVis = Visibility.Hidden;
        private Visibility _loadingProductInfoVis = Visibility.Hidden;
        private Visibility _loadingVariantVis = Visibility.Hidden;
        private Visibility _loadingProductImages = Visibility.Hidden;
        private BindableCollection<ProductRowItem> _rowItemProducts;
        private BindableCollection<ProductVariant> _rowItemVariants;
        private BindableCollection<ProductImage> _rowItemImages;
        private ProductRowItem _selectedProduct;
        private ProductVariant _selectedVariant;
        private ProductImage _selectedImage;
        private ProductDetails _productDetails;
        private Pagination _pagination;
        private IWindowManager _windowManager;
        private BindableCollection<Category> _categoryList;
        private BindableCollection<ProductType> _typeList;
        private Category _selectedCategory;
        private bool _editing = false;
        private int _totalVariants;
        private int _totalImages;
        private ProductDetails _productInfoBeforeEditing;

        #region Base
        public ProductViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            Pagination = new Pagination(10, this);
            _productInfoBeforeEditing = new ProductDetails();
        }
        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _restClient = RestConnection.ManagementRestClient;
            _ = LoadPageAsync();
            return Task.CompletedTask;
        }
        public Task LoadPageAsync()
        {
            if (RowItemProducts != null)
                RowItemProducts.Clear();

            LoadingPageVis = Visibility.Visible;

            _ = LoadCategoryList();
            _ = LoadTypeList();

            var list = new List<KeyValuePair<string, string>>() {
                      new KeyValuePair<string, string>("page", Pagination.CurrentPage.ToString()),
                      new KeyValuePair<string, string>("size", Pagination.PageSize.ToString()),
                };
            var response = RestConnection.ExecuteParameterRequestAsync(_restClient, Method.Get, "products", list);

            if ((int)response.Result.StatusCode == 200)
            {
                var products = response.Result.Content;
                RowItemProducts = JsonConvert.DeserializeObject<BindableCollection<ProductRowItem>>(products);
                Pagination.UpdatePaginationStatus(response.Result.Headers);
            }
            NotifyOfPropertyChange(() => Pagination);

            LoadingPageVis = Visibility.Hidden;
            return Task.CompletedTask;
        }
        private async Task LoadTypeList()
        {
            var typeList = Lists.ProductTypeList.LoadTypeList();
            TypeList = new BindableCollection<ProductType>((await typeList));
        }
        private async Task LoadCategoryList()
        {
            var categoryList = Lists.CategoryList.LoadCategoryList();
            CategoryList = new BindableCollection<Category>((await categoryList));
        }
        private Task LoadDetailItemAsync(int id)
        {
            LoadingInfoVis = Visibility.Visible;
            LoadingVariantVis = Visibility.Visible;
            LoadingProductImages = Visibility.Visible;

            _ = LoadProductInformation(id);
            _ = LoadVariants(id);
            _ = LoadProductImages(id);

            return Task.CompletedTask;
        }
        private async Task LoadProductInformation(int id)
        {
            var request = new RestRequest($"products/{id}", Method.Get);
            var respone = await _restClient.ExecuteAsync(request);
            if ((int)respone.StatusCode == 200)
            {
                var productDetails = respone.Content;
                ProductDetails = JsonConvert.DeserializeObject<ProductDetails>(productDetails);
            }
            LoadingInfoVis = Visibility.Hidden;
        }
        private async Task LoadVariants(int id)
        {
            var request = new RestRequest($"products/variants/{id}", Method.Get);
            var respone = await _restClient.ExecuteAsync(request);
            if ((int)respone.StatusCode == 200)
            {
                var variants = respone.Content;
                RowItemVariants = JsonConvert.DeserializeObject<BindableCollection<ProductVariant>>(variants);
            }
            LoadingVariantVis = Visibility.Hidden;
        }
        private async Task LoadProductImages(int id)
        {
            var request = new RestRequest($"products/{id}/images", Method.Get);
            var respone = await _restClient.ExecuteAsync(request);
            if ((int)respone.StatusCode == 200)
            {
                var productImages = respone.Content;
                RowItemImages = JsonConvert.DeserializeObject<BindableCollection<ProductImage>>(productImages);
            }
            LoadingProductImages = Visibility.Hidden;
        }
        public void RequestUpdateProductInfo()
        {
            Editing = true;
            _productInfoBeforeEditing = new ProductDetails(ProductDetails);
        }
        public async Task UpdateProductInfoAsync()
        {
            if (ProductInfoHasErrors()) return;

            LoadingInfoVis = Visibility.Visible;
            string JSonProductInfo = StringUtils.SerializeObject(ProductDetails);
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Put, $"products/info", JSonProductInfo, "application/json");
            int statusCode = (int)response.StatusCode;
            string responseBody = response.Content;
            switch (statusCode)
            {
                case 200:
                    ShowSuccessMessage("Cập nhật sản phẩm thành công");
                    Editing = false;
                    break;
                case 404 when ProductNotFound(responseBody):
                    await ShowErrorMessage("Lỗi cập nhật", "Sản phẩm không còn tồn tài, vui lòng tải lại trang");
                    break;
                case 404 when CategoryNotFound(responseBody):
                    await ShowErrorMessage("Lỗi cập nhật", "Danh mục không còn tồn tại, vui lòng tải lại trang");
                    break;
                case 400:
                    await ShowErrorMessage("Lỗi cập nhật", "Tên sản phẩm đã tồn tại, vui lòng chọn tên khác");
                    break;
            }
            LoadingInfoVis = Visibility.Hidden;
        }

        private bool ProductInfoHasErrors()
        {
            return string.IsNullOrEmpty(ProductDetails.Name);
        }

        private bool ProductNotFound(string responseBody)
        {
            return responseBody.Contains("product") && responseBody.Contains("notFoundId");
        }
        private bool CategoryNotFound(string responseBody)
        {
            return responseBody.Contains("category") && responseBody.Contains("notFoundId");
        }
        public void CancelUpdate()
        {
            Editing = false;
            ProductDetails = new ProductDetails(_productInfoBeforeEditing);
        }
        public void ShowAddingProductDialog()
        {
            _windowManager.ShowDialogAsync(new NewProductViewModel());
        }

        #endregion

        #region Events
        public void Expander_Expanded()
        {
            if (SelectedProduct != null)
                _ = LoadDetailItemAsync(SelectedProduct.Id);
        }
        public void RowItemProducts_SelectionChanged()
        {
            View.Dispatcher.Invoke(() =>
            {
                if (View.RowItemProducts.SelectedIndex < 0)
                {
                    ProductDetails = null;
                }
            });
        }
        #endregion

        #region Pagination
        public void LoadFirstPage()
        {
            Pagination.LoadFirstPage();
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
        public ProductView View => (ProductView)this.GetView();
        public BindableCollection<ProductRowItem> RowItemProducts
        {
            get
            {
                return _rowItemProducts;
            }
            set
            {
                _rowItemProducts = value;
                NotifyOfPropertyChange(() => RowItemProducts);
            }
        }
        public BindableCollection<ProductVariant> RowItemVariants
        {
            get => _rowItemVariants;
            set
            {
                _rowItemVariants = value;
                if (value != null)
                {
                    TotalVariants = value.Count;
                }
                NotifyOfPropertyChange(() => RowItemVariants);
            }
        }
        public BindableCollection<ProductImage> RowItemImages
        {
            get => _rowItemImages;
            set
            {
                _rowItemImages = value;
                if (value != null)
                {
                    TotalImages = value.Count;
                }
                NotifyOfPropertyChange(() => RowItemImages);
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
            get { return _loadingProductInfoVis; }
            set
            {
                _loadingProductInfoVis = value;
                NotifyOfPropertyChange(() => LoadingInfoVis);
            }
        }
        public Visibility LoadingVariantVis
        {
            get => _loadingVariantVis;
            set
            {
                _loadingVariantVis = value;
                NotifyOfPropertyChange(() => LoadingVariantVis);
            }
        }
        public Visibility LoadingProductImages
        {
            get => _loadingProductImages;
            set
            {
                _loadingProductImages = value;
                NotifyOfPropertyChange(() => LoadingProductImages);
            }
        }
        public ProductRowItem SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                if (value != null && View.DetailExpander.IsExpanded)
                    _ = LoadDetailItemAsync(value.Id);
                NotifyOfPropertyChange(() => SelectedProduct);
            }
        }
        public ProductVariant SelectedVariant
        {
            get => _selectedVariant;
            set
            {
                _selectedVariant = value;
                NotifyOfPropertyChange(() => SelectedVariant);
            }
        }
        public ProductImage SelectedImage
        {
            get => _selectedImage;
            set
            {
                _selectedImage = value;
                NotifyOfPropertyChange(() => SelectedImage);
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
        public BindableCollection<ProductType> TypeList
        {
            get => _typeList;
            set
            {
                _typeList = value;
                NotifyOfPropertyChange(() => TypeList);
            }
        }
        public bool Editing
        {
            get => _editing;
            set
            {
                _editing = value;
                NotifyOfPropertyChange(() => Editing);
                NotifyOfPropertyChange(() => NotEditing);
            }
        }
        public bool NotEditing => !Editing;
        public int TotalVariants
        {
            get => _totalVariants;
            set
            {
                _totalVariants = value;
                NotifyOfPropertyChange(() => TotalVariants);
            }
        }
        public int TotalImages
        {
            get => _totalImages;
            set
            {
                _totalImages = value;
                NotifyOfPropertyChange(() => TotalImages);
            }
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
            MessageUtils.ShowSuccessMessage(View, View.GreenMessage, View.GreenSB, View.GreenContent, message);
        }
        #endregion


    }
}
