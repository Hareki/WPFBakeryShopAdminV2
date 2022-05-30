using Caliburn.Micro;
using RestSharp;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
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

        #region Base
        public NewProductViewModel(ProductViewModel productViewModel, IWindowManager windowManager)
        {
            _productViewModel = productViewModel;
            _windowManager = windowManager;
        }
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
                await ShowErrorMessage("Lỗi tải danh mục", "Không tìm thấy danh mục nào, vui lòng thử lại sau");
                CancelAdding();
                return;
            }
            if (ProductInfoHasErrors()) {
                await ShowErrorMessage("Lỗi nhập liệu", "Vui lòng nhập đầy đủ thông tin và đúng định dạng");
                return;
            }

            ProductDetails.CategoryId = SelectedCategory.Id;
            string JsonProductInfo = StringUtils.SerializeObject(ProductDetails);
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Post, "products", JsonProductInfo, "application/json");
            int statusCode = (int)response.StatusCode;
            switch (statusCode)
            {
                case 200:
                    _productViewModel.LoadLastPage();
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
        }

        private bool ProductInfoHasErrors()
        {
            bool result =  string.IsNullOrEmpty(ProductDetails.Name) ||
                string.IsNullOrEmpty(ProductDetails.Allergens) ||
                string.IsNullOrEmpty(ProductDetails.Ingredients);

            return result;
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
            await MessageUtils.ShowErrorMessageInDialog(title, message, _windowManager);
        }
        private void ShowSuccessMessage(string message)
        {
            MessageUtils.ShowSnackBarMessage(View, View.GreenMessage, View.GreenSB, View.GreenContent, message, View.RedSB);
        }
        #endregion
    }
}
