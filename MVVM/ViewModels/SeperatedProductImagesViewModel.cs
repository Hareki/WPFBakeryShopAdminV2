using Caliburn.Micro;
using System.Collections;
using System.Windows.Controls;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;
using WPFBakeryShopAdminV2.MVVM.Views;

namespace WPFBakeryShopAdminV2.MVVM.ViewModels
{
    public class SeperatedProductImagesViewModel : Screen
    {
        private BindableCollection<ProductImage> _rowItemImages;
        private IList _selectedItems;
        private ProductViewModel _productViewModel;

        public SeperatedProductImagesViewModel(BindableCollection<ProductImage> rowItemImages, IList selectedItems, ProductViewModel productViewModel, ProductImage selectedImage = null)
        {
            RowItemImages = rowItemImages;
            InnerGridSelectedItems = selectedItems;
            _productViewModel = productViewModel;
        }
        public void Cancel()
        {
            this.TryCloseAsync(false);
        }
        public void ConfirmSelecting()
        {
            this.TryCloseAsync(true);//mặc định khi không truyền sẽ là false
        }
        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);
            SetSelectedItemsFromInnerGrid();
        }

        public void RowItemImages_SelectionChanged()
        {
            _productViewModel.SetSelectedItems(View.RowItemImages.SelectedItems);
        }

        private void SetSelectedItemsFromInnerGrid()
        {
            foreach (var item in InnerGridSelectedItems)
            {
                Grid.SelectedItems.Add(item);
            }
        }
        public BindableCollection<ProductImage> RowItemImages
        {
            get => _rowItemImages;
            set
            {
                _rowItemImages = value;
                NotifyOfPropertyChange(() => RowItemImages);
            }
        }
        public IList InnerGridSelectedItems
        {
            get => _selectedItems;
            set
            {
                _selectedItems = value;
                NotifyOfPropertyChange(() => InnerGridSelectedItems);
            }
        }
        public SeperatedProductImagesView View => (SeperatedProductImagesView)this.GetView();
        public DataGrid Grid
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
    }
}
