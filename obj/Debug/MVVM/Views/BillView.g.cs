﻿#pragma checksum "..\..\..\..\MVVM\Views\BillView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "68EA217BC83D34B6C813BF8037A2DE3706636F0DD16E1AE30AF16F4443A4FD34"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
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
using WPFBakeryShopAdminV2.Converters;
using WPFBakeryShopAdminV2.MVVM.ViewModels;
using WPFBakeryShopAdminV2.MVVM.Views;


namespace WPFBakeryShopAdminV2.MVVM.Views {
    
    
    /// <summary>
    /// BillView
    /// </summary>
    public partial class BillView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Transitions.TransitioningContent TrainsitionigContentSlide;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ReloadPage;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid RowItemBills;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoadLastPage;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoadNextPage;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoadPreviousPage;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoadFirstPage;
        
        #line default
        #line hidden
        
        
        #line 137 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Pagination_PageIndicator;
        
        #line default
        #line hidden
        
        
        #line 152 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Snackbar GreenSB;
        
        #line default
        #line hidden
        
        
        #line 160 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel GreenContent;
        
        #line default
        #line hidden
        
        
        #line 166 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock GreenMessage;
        
        #line default
        #line hidden
        
        
        #line 172 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.DialogHost DialogContainer;
        
        #line default
        #line hidden
        
        
        #line 180 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border DialogContent;
        
        #line default
        #line hidden
        
        
        #line 183 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ConfirmContent;
        
        #line default
        #line hidden
        
        
        #line 191 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ConfirmTitleTB;
        
        #line default
        #line hidden
        
        
        #line 198 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ConfirmMessageTB;
        
        #line default
        #line hidden
        
        
        #line 235 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ErrorContent;
        
        #line default
        #line hidden
        
        
        #line 243 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ErrorTitleTB;
        
        #line default
        #line hidden
        
        
        #line 249 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ErrorMessageTB;
        
        #line default
        #line hidden
        
        
        #line 282 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Expander DetailExpander;
        
        #line default
        #line hidden
        
        
        #line 332 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid BillDetails_Details;
        
        #line default
        #line hidden
        
        
        #line 378 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BillDetails_FormattedTotal;
        
        #line default
        #line hidden
        
        
        #line 393 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BillDetails_CustomerName;
        
        #line default
        #line hidden
        
        
        #line 488 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BillDetails_FormattedPurchaseDate;
        
        #line default
        #line hidden
        
        
        #line 502 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BillDetails_PaidMethodName;
        
        #line default
        #line hidden
        
        
        #line 540 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BillDetails_ReceiverInfo_Address;
        
        #line default
        #line hidden
        
        
        #line 572 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BillDetails_ReceiverInfo_Phone;
        
        #line default
        #line hidden
        
        
        #line 602 "..\..\..\..\MVVM\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BillDetails_ReceiverInfo_Note;
        
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
            System.Uri resourceLocater = new System.Uri("/WPFBakeryShopAdminV2;component/mvvm/views/billview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\MVVM\Views\BillView.xaml"
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
            this.TrainsitionigContentSlide = ((MaterialDesignThemes.Wpf.Transitions.TransitioningContent)(target));
            return;
            case 2:
            this.ReloadPage = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.RowItemBills = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.LoadLastPage = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.LoadNextPage = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.LoadPreviousPage = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.LoadFirstPage = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.Pagination_PageIndicator = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.GreenSB = ((MaterialDesignThemes.Wpf.Snackbar)(target));
            return;
            case 10:
            this.GreenContent = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 11:
            this.GreenMessage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            this.DialogContainer = ((MaterialDesignThemes.Wpf.DialogHost)(target));
            return;
            case 13:
            this.DialogContent = ((System.Windows.Controls.Border)(target));
            return;
            case 14:
            this.ConfirmContent = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 15:
            this.ConfirmTitleTB = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 16:
            this.ConfirmMessageTB = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 17:
            this.ErrorContent = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 18:
            this.ErrorTitleTB = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 19:
            this.ErrorMessageTB = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 20:
            this.DetailExpander = ((System.Windows.Controls.Expander)(target));
            return;
            case 21:
            this.BillDetails_Details = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 22:
            this.BillDetails_FormattedTotal = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 23:
            this.BillDetails_CustomerName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 24:
            this.BillDetails_FormattedPurchaseDate = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 25:
            this.BillDetails_PaidMethodName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 26:
            this.BillDetails_ReceiverInfo_Address = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 27:
            this.BillDetails_ReceiverInfo_Phone = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 28:
            this.BillDetails_ReceiverInfo_Note = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

