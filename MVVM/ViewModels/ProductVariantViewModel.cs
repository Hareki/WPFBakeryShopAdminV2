using Caliburn.Micro;
using RestSharp;
using System.Threading.Tasks;
using System.Windows;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;
using WPFBakeryShopAdminV2.MVVM.Views;
using WPFBakeryShopAdminV2.Utilities;

namespace WPFBakeryShopAdminV2.MVVM.ViewModels
{
    public class ProductVariantViewModel : Screen
    {
        private bool _editMode;
        private ProductViewModel _productViewModel;
        private ProductVariant _productVariant;
        private BindableCollection<ProductType> _typeList;
        private ProductType _selectedProductType;
        internal RestClient _restClient;
        private string _productName;


        public ProductVariantViewModel(ProductViewModel productViewModel, bool editMode, ProductVariant productVariant, BindableCollection<ProductType> typeList, string productName)
        {
            EditMode = editMode;
            _productViewModel = productViewModel;
            ProductVariant = productVariant;
            TypeList = typeList;
            SelectedProductType = TypeList[0];
            _restClient = RestConnection.ManagementRestClient;
            ProductName = productName;
        }
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            if (ProductVariant == null) _productVariant = new ProductVariant();
            if (EditMode)
            {
                View.DummyId.Visibility = Visibility.Hidden;

            }
            else
            {
                View.ProductVariant_Id.Visibility = Visibility.Hidden;
            }
        }
        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);
            if (EditMode)
            {
                View.DialogMainTitle.Text = "Sửa loại sản phẩm";
            }
            else
            {
                View.DialogMainTitle.Text = "Thêm loại sản phẩm";
            }
        }

        public async Task AddEditProductVariant()
        {
            ProductVariant.Cost = (int)View.ProductCost.Value;
            ProductVariant.Price = (int)View.ProductPrice.Value;
            ProductVariant.TypeId = SelectedProductType.Id;
            if (ProductVariant.Price <= ProductVariant.Cost)
            {
                ShowErrorMessage(null, "Giá bán phải lớn hơn giá gốc");
                return;
            }

            string JSonProductVariant = StringUtils.SerializeObject(ProductVariant);
            if (EditMode)
            {
                await EditProductVariant(JSonProductVariant);
            }
            else
            {
                await AddProductVariant(JSonProductVariant);
            }

        }

        private bool ProducIdNotFound(string response)
        {
            return response.Contains("product") && response.Contains("notFoundId");
        }
        private bool ProductTypeNotFound(string response)
        {
            return response.Contains("product") && response.Contains("notFoundTypeId");
        }
        private bool VariantAlreadyExists(string response)
        {
            bool bool1 = response.Contains("product");
            bool bool2 = response.Contains("existedVariant");
            return bool1 && bool2;
        }
        private bool VariantIdNotFound(string response)
        {
            return response.Contains("product") && response.Contains("notFoundIdVariant");
        }
        public async Task AddProductVariant(string requestBody)
        {
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Post, "variants", requestBody, "application/json");
            int statusCode = (int)response.StatusCode;
            string responseBody = response.Content;
            switch (statusCode)
            {
                case 200:
                    ShowSuccessMessage("Thêm loại sản phẩm thành công");
                    await _productViewModel.LoadVariantsAsync(ProductVariant.ProductId);
                    _productViewModel.FocusProductVariant(null);
                    ResetFields();
                    break;
                case 404 when ProducIdNotFound(responseBody):
                    ShowErrorMessage(null, "Sản phẩm này không còn tồn tại, vui lòng tải lại trang");
                    break;
                case 404 when ProductTypeNotFound(responseBody):
                    ShowErrorMessage(null, "Loại sản phẩm này không còn tồn tại, vui lòng tải lại trang");
                    break;
                case 400 when VariantAlreadyExists(responseBody):
                    ShowErrorMessage(null, "Biến thể với sản phẩm và loại sản phẩm này đã tồn tại");
                    break;
            }
        }

        private void ResetFields()
        {
            ProductVariant = new ProductVariant(ProductVariant.ProductId);
            SelectedProductType = TypeList[0];
        }

        public async Task EditProductVariant(string requestBody)
        {
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Put, "variants", requestBody, "application/json");
            int statusCode = (int)response.StatusCode;
            string responseBody = response.Content;
            switch (statusCode)
            {
                case 200:
                    ShowSuccessMessage("Cập nhật biến thể thành công");
                    await _productViewModel.LoadVariantsAsync(ProductVariant.ProductId);
                    Cancel();
                    break;
                case 404 when ProducIdNotFound(responseBody):
                    ShowErrorMessage(null, "Sản phẩm này không còn tồn tại, vui lòng tải lại trang");
                    break;
                case 404 when ProductTypeNotFound(responseBody):
                    ShowErrorMessage(null, "Loại sản phẩm này không còn tồn tại, vui lòng tải lại trang");
                    break;
                case 400 when VariantAlreadyExists(responseBody):
                    ShowErrorMessage(null, "Biến thể với sản phẩm và loại sản phẩm này đã tồn tại");
                    break;
                case 400 when VariantIdNotFound(responseBody):
                    ShowErrorMessage(null, "Biến thể này không còn tồn tại, vui lòng tải lại trang");
                    break;
            }

        }
        public void Cancel()
        {
            this.TryCloseAsync(true);
        }

        #region Binding Properties
        public bool EditMode
        {
            get => _editMode; set
            {
                _editMode = value;
                NotifyOfPropertyChange(() => EditMode);
            }
        }
        public ProductVariantView View => (ProductVariantView)this.GetView();
        public ProductVariant ProductVariant
        {
            get => _productVariant;
            set
            {
                _productVariant = value;
                NotifyOfPropertyChange(() => ProductVariant);
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
        public ProductType SelectedProductType
        {
            get => _selectedProductType; set
            {
                _selectedProductType = value;
                NotifyOfPropertyChange(() => SelectedProductType);
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
        #endregion

        #region Show Messages
        private void ShowErrorMessage(string title, string message)
        {
            MessageUtils.ShowSnackBarMessage(View, View.RedMessage, View.RedSB, View.RedContent, message);
        }
        private void ShowSuccessMessage(string message)
        {
            MessageUtils.ShowSnackBarMessage(View, View.GreenMessage, View.GreenSB, View.GreenContent, message);
        }
        #endregion
    }
}
