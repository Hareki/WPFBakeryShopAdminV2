﻿#pragma checksum "..\..\..\..\MVVM\Views\ProductVariantView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "AC677A132601CC5D61C770FDD41696FBDF0B1E04E4B3647B634116880B6C774F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WPFBakeryShopAdminV2.MVVM.ViewModels;
using WPFBakeryShopAdminV2.MVVM.Views;


namespace WPFBakeryShopAdminV2.MVVM.Views {
    
    
    /// <summary>
    /// ProductVariantView
    /// </summary>
    public partial class ProductVariantView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 43 "..\..\..\..\MVVM\Views\ProductVariantView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock DialogMainTitle;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\..\MVVM\Views\ProductVariantView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ProductVariant_Id;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\..\MVVM\Views\ProductVariantView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TypeList;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\..\MVVM\Views\ProductVariantView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ProductVariant_Cost;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\..\..\MVVM\Views\ProductVariantView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ProductVariant_Price;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WPFBakeryShopAdminV2;component/mvvm/views/productvariantview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\MVVM\Views\ProductVariantView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.DialogMainTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.ProductVariant_Id = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.TypeList = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.ProductVariant_Cost = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.ProductVariant_Price = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
