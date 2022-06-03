using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Newtonsoft.Json;
using RestSharp;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFBakeryShopAdminV2.Interfaces;
using WPFBakeryShopAdminV2.LocalValidationRules;
using WPFBakeryShopAdminV2.MVVM.Models.Bodies;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;
using WPFBakeryShopAdminV2.MVVM.Views;
using WPFBakeryShopAdminV2.Utilities;
using LangStr = WPFBakeryShopAdminV2.Utilities.Language;

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
        private bool _editingProductInformation = false;
        private int _totalVariants;
        private int _totalImages;
        private ProductDetails _productInfoBeforeEditing;
        private bool _activated = false;

        #region Base
        public ProductViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            Pagination = new Pagination(10, this);
            _productInfoBeforeEditing = new ProductDetails();
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
        public async Task ReloadPageAsync()
        {
            Pagination.CurrentPage = 0;
            await LoadPageAsync();
        }
        public async Task LoadPageAsync()
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
            var response = await RestConnection.ExecuteParameterRequestAsync(_restClient, Method.Get, "products", list);

            if ((int)response.StatusCode == 200)
            {
                var products = response.Content;
                RowItemProducts = JsonConvert.DeserializeObject<BindableCollection<ProductRowItem>>(products);
                Pagination.UpdatePaginationStatus(response.Headers);
            }
            NotifyOfPropertyChange(() => Pagination);

            LoadingPageVis = Visibility.Hidden;
        }
        private async Task LoadTypeList()
        {
            var typeList = Lists.ProductTypeList.LoadTypeList();
            TypeList = new BindableCollection<ProductType>((await typeList));
        }
        private async Task LoadCategoryList()
        {
            View.CategoryList.IsEnabled = false;
            HintAssist.SetHint(View.CategoryList, LangStr.Get("Product_LoadingCategory"));

            var categoryList = Lists.CategoryList.LoadCategoryList();
            CategoryList = new BindableCollection<Category>((await categoryList));

            if (View != null)
                View.CategoryList.IsEnabled = true;
            HintAssist.SetHint(View.CategoryList, LangStr.Get("PD_Category"));
        }
        private Task LoadDetailItemAsync(int id)
        {
            LoadingInfoVis = Visibility.Visible;
            LoadingVariantVis = Visibility.Visible;
            LoadingProductImages = Visibility.Visible;

            _ = LoadProductInformationAsync(id);
            _ = LoadVariantsAsync(id);
            _ = LoadProductImagesAsync(id);

            return Task.CompletedTask;
        }
        private async Task LoadProductInformationAsync(int id)
        {
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Get, $"products/{id}");
            if ((int)response.StatusCode == 200)
            {
                var productDetails = response.Content;
                ProductDetails = JsonConvert.DeserializeObject<ProductDetails>(productDetails);
            }
            LoadingInfoVis = Visibility.Hidden;
        }
        public async Task LoadVariantsAsync(int id)
        {
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Get, $"products/variants/{id}");
            if ((int)response.StatusCode == 200)
            {
                var variants = response.Content;
                RowItemVariants = JsonConvert.DeserializeObject<BindableCollection<ProductVariant>>(variants);
            }
            LoadingVariantVis = Visibility.Hidden;
        }
        public async Task LoadProductImagesAsync(int id)
        {
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Get, $"products/{id}/images");
            if ((int)response.StatusCode == 200)
            {
                var productImages = response.Content;
                RowItemImages = JsonConvert.DeserializeObject<BindableCollection<ProductImage>>(productImages);
                ImagesGrid.SelectedItems.Clear();
            }
            LoadingProductImages = Visibility.Hidden;
        }
        #endregion

        #region Validations
        private bool ProductNotFound(string responseBody)
        {
            return responseBody.Contains("product") && responseBody.Contains("notFoundId");
        }
        private bool CategoryNotFound(string responseBody)
        {
            return responseBody.Contains("category") && responseBody.Contains("notFoundId");
        }
        private bool InformationHasErrors()
        {
            bool test2 = NotEmptyValidationRule.TryNotifyEmptyField(View.txtIngredients);
            bool test3 = NotEmptyValidationRule.TryNotifyEmptyField(View.txtName);
            View.UpdateInformationAsync.Focus();
            return test2 || test3;
        }
        #endregion

        #region Product Variants
        public void ShowNewVariantDialog()
        {
            _windowManager.ShowDialogAsync(new ProductVariantViewModel(this, false, new ProductVariant(SelectedProduct.Id), TypeList, SelectedProduct.Name, _windowManager));
        }
        public void ShowEditVariantDialog()
        {
            _windowManager.ShowDialogAsync(new ProductVariantViewModel(this, true, SelectedVariant, TypeList, SelectedProduct.Name, _windowManager));
        }
        public async Task DeleteVariant()
        {
            if (SelectedVariant == null) return;


            bool confirm = await ShowConfirmMessage(LangStr.Get("Message_ConfirmationTitle"), LangStr.Get("Product_ConfirmDeletingVariant") + $" {SelectedProduct.Name} {SelectedVariant.TypeName}?");
            if (confirm)
            {
                LoadingVariantVis = Visibility.Visible;

                var result = await RestConnection.ExecuteRequestAsync(_restClient, Method.Delete, $"variants/{SelectedVariant.Id}");
                int statusCode = (int)result.StatusCode;

                if (statusCode == 200)
                {
                    await LoadVariantsAsync(SelectedProduct.Id);
                    LoadingVariantVis = Visibility.Hidden;
                    ShowSuccessMessage(LangStr.Get("Product_RemoveVariant200"));
                }
                else if (statusCode == 404)
                {
                    LoadingVariantVis = Visibility.Hidden;
                    await ShowErrorMessage(LangStr.Get("Message_ErrorTitle"), LangStr.Get("PV_EditVariantNoExists"));

                }
                else if (statusCode == 400)
                {
                    LoadingVariantVis = Visibility.Hidden;
                    await ShowErrorMessage(LangStr.Get("Message_ErrorTitle"), LangStr.Get("Product_SoldCantBeDeleted"));
                }
            }

        }
        public void FocusProductVariant(int variantid)
        {
            DataGrid grid = View.RowItemVariants;
            if (variantid != -1)
            {
                List<ProductVariant> currenItems = grid.Items.OfType<ProductVariant>().ToList();
                int index = currenItems.FindIndex((element) => element.Id == variantid);
                grid.SelectedIndex = index;
                grid.ScrollIntoView(grid.SelectedItem);
            }
            else
            {
                grid.SelectedIndex = grid.Items.Count - 1;
                grid.ScrollIntoView(grid.SelectedItem);
            }

        }
        #endregion

        #region Product Information
        private void RefreshProductRowItemGrid(ProductRowItem productRowItem)
        {
            View.Dispatcher.Invoke(() =>
            {
                int selectedRow = View.RowItemProducts.SelectedIndex;

                RowItemProducts[selectedRow].SetProperties(productRowItem);
                View.RowItemProducts.Items.Refresh();
                //View.RowItemProducts.SelectedIndex = selectedRow; tắt dòng này để tránh call lại API, để selectedRow ko đổi thì item tại row đó ko dc gán = object mới, mà chỉ dc sửa lại thuộc tính
            });
        }
        public void CancelUpdatingInformation()
        {
            EditingInformation = false;
            ProductDetails = new ProductDetails(_productInfoBeforeEditing);
        }
        public async Task UpdateInformationAsync()
        {
            if (InformationHasErrors()) return;
            LoadingInfoVis = Visibility.Visible;
            ProductDetails.CategoryId = SelectedCategory.Id;

            ProductDetails.Name = StringUtils.Trim(ProductDetails.Name);
            ProductDetails.Ingredients = StringUtils.Trim(ProductDetails.Ingredients);
            ProductDetails.Allergens = StringUtils.Trim(ProductDetails.Allergens);
            NotifyOfPropertyChange(() => ProductDetails);

            string JSonProductInfo = StringUtils.SerializeObject(ProductDetails);
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Put, $"products/info", JSonProductInfo, "application/json");
            int statusCode = (int)response.StatusCode;
            string responseBody = response.Content;
            switch (statusCode)
            {
                case 200:
                    RefreshProductRowItemGrid(ProductDetails.ProductRowItem);
                    ShowSuccessMessage(LangStr.Get("Product_UpdateInfor200"));
                    EditingInformation = false;
                    break;
                case 404 when ProductNotFound(responseBody):
                    await ShowErrorMessage(LangStr.Get("Message_ErrorTitle"), LangStr.Get("PV_Add404Product"));
                    break;
                case 404 when CategoryNotFound(responseBody):
                    await ShowErrorMessage(LangStr.Get("Message_ErrorTitle"), LangStr.Get("Product_CategoryNoExists"));
                    break;
                case 400:
                    await ShowErrorMessage(LangStr.Get("Message_ErrorTitle"), LangStr.Get("Product_NameUsed"));
                    break;
            }
            LoadingInfoVis = Visibility.Hidden;
        }
        public void RequestUpdatingInformation()
        {
            EditingInformation = true;
            _productInfoBeforeEditing = new ProductDetails(ProductDetails);
        }
        public void FocusProductRowItem(ProductRowItem productRowItem)
        {
            List<ProductRowItem> currenItems = View.RowItemProducts.Items.OfType<ProductRowItem>().ToList();
            int index = currenItems.FindIndex((element) => element.Name == productRowItem.Name);
            View.RowItemProducts.SelectedIndex = index;
        }
        public async Task ShowAddingProductDialog()
        {
            await _windowManager.ShowDialogAsync(new NewProductViewModel(this, _windowManager, CategoryList));
        }
        #endregion

        #region Product Images
        public async Task AddImagesAsync()
        {

            OpenFileDialog open = Utilities.Constants.OpenFileDialog;
            var images = new List<KeyValuePair<string, string>>();
            if ((bool)open.ShowDialog())
            {
                LoadingProductImages = Visibility.Visible;
                foreach (string element in open.FileNames)
                {
                    images.Add(new KeyValuePair<string, string>("images", element));
                }
                var response = await RestConnection.ExecuteFileRequestAsync(_restClient, Method.Post, $"products/{SelectedProduct.Id}/images", images);
                int statusCode = (int)response.StatusCode;
                LoadingProductImages = Visibility.Hidden;
                if (statusCode == 201)
                {
                    ShowSuccessMessage(LangStr.Get("Product_ImageAdded200"));
                    await LoadProductImagesAsync(SelectedProduct.Id);
                }
                else
                {
                    await ShowErrorMessage(LangStr.Get("Message_ErrorTitle"), LangStr.Get("UnexpectedError"));
                    Debug.Assert(false);
                }

            }
        }
        public async Task ConfirmDeleteImages()
        {
            bool confirm = await ShowConfirmMessage(LangStr.Get("Message_ConfirmationTitle"), LangStr.Get("Product_ConfirmDeletingImage"));
            if (!confirm) return;
            await DeleteImagesAsync();
        }

        public async Task DeleteImagesAsync()
        {
            LoadingProductImages = Visibility.Visible;
            List<ProductImage> list = ImagesGrid.SelectedItems.Cast<ProductImage>().ToList();

            DeleteImagesBody deleteImagesBody = new DeleteImagesBody
            {
                DeletedImageIds = (from ProductImage image in list
                                   select image.Id).ToList()
            };
            string requestBody = StringUtils.SerializeObject(deleteImagesBody);
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Delete, $"products/{SelectedProduct.Id}/images", requestBody, "application/json");

            LoadingProductImages = Visibility.Hidden;
            int statusCode = (int)response.StatusCode;
            switch (statusCode)
            {
                case 200:
                    ShowSuccessMessage(LangStr.Get("Product_DeleteImage200"));
                    await LoadProductImagesAsync(SelectedProduct.Id);
                    break;
                case 404:
                    await ShowErrorMessage(LangStr.Get("Message_ErrorTitle"), LangStr.Get("Product_ImageNoExists"));
                    break;
                case 400:
                    await ShowErrorMessage(LangStr.Get("Message_ErrorTitle"), LangStr.Get("Product_ImageNoBelong"));
                    break;
            }

        }

        public async Task ShowSeperatedProductImagesDialogAsync()
        {
            await _windowManager.ShowDialogAsync(new SeperatedProductImagesViewModel(this, SelectedProduct?.Name, TotalImages,
                View.ConfirmDeleteImages.IsEnabled, View.AddImagesAsync.IsEnabled, _windowManager, _restClient));
        }
        internal void SetSelectedItems(IList OuterSelectedItems)
        {
            ImagesGrid.SelectedItems.Clear();
            foreach (var item in OuterSelectedItems)
            {
                ImagesGrid.SelectedItems.Add(item);
            }
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
        public void RowItemImages_SelectionChanged()
        {
            View.ConfirmDeleteImages.IsEnabled = (ImagesGrid.SelectedItems.Count > 0);
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
                else
                {
                    RowItemVariants = null;
                    RowItemImages = null;
                }
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
        public bool EditingInformation
        {
            get => _editingProductInformation;
            set
            {
                _editingProductInformation = value;
                NotifyOfPropertyChange(() => EditingInformation);
                NotifyOfPropertyChange(() => NotEditingInformation);
            }
        }
        public bool NotEditingInformation => !EditingInformation;
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
        public DataGrid ImagesGrid => View.RowItemImages;
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
        public void ShowSuccessMessage(string message)
        {
            MessageUtils.ShowSnackBarMessage(View, View.GreenMessage, View.GreenSB, View.GreenContent, message);
        }
        #endregion


    }
}
