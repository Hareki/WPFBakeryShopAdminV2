using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;
using WPFBakeryShopAdminV2.MVVM.Views;

namespace WPFBakeryShopAdminV2.MVVM.ViewModels
{
    public class ProductVariantViewModel : Screen
    {
        private bool _editMode;
        private ProductViewModel _productViewModel;
        private ProductVariant _productVariant;
        private BindableCollection<ProductType> _typeList;
        private ProductType _selectedProductType;

        public ProductVariantViewModel(ProductViewModel productViewModel, bool editMode, ProductVariant productVariant, BindableCollection<ProductType> typeList)
        {
            EditMode = editMode;
            _productViewModel = productViewModel;
            ProductVariant = productVariant;
            TypeList = typeList;
        }
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            if (ProductVariant == null) _productVariant = new ProductVariant();
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
    }
}
