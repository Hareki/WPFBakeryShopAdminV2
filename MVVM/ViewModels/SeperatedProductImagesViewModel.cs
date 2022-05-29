using Caliburn.Micro;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;
using WPFBakeryShopAdminV2.MVVM.Views;

namespace WPFBakeryShopAdminV2.MVVM.ViewModels
{
    public class SeperatedProductImagesViewModel : Screen
    {
        private BindableCollection<ProductImage> _rowItemImages;
        private IList _selectedItems;
        public SeperatedProductImagesViewModel(BindableCollection<ProductImage> rowItemImages, IList selectedItems)
        {
            RowItemImages = rowItemImages;
            SelectedItems = selectedItems;
           // View.RowItemImages.selecte
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
        public IList SelectedItems
        {
            get => _selectedItems;
            set
            {
                _selectedItems = value;
                NotifyOfPropertyChange(() => SelectedItems);
            }
        }
        public SeperatedProductImagesView View => (SeperatedProductImagesView)this.GetView();
    }
}
