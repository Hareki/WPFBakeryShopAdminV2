﻿#pragma checksum "..\..\..\..\MVVM\Views\AccountView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0F67A7B3AEF3A65FE445B927627BD37D8CD3B80291D64114A6F123DD02D96874"
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
    /// AccountView
    /// </summary>
    public partial class AccountView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\..\MVVM\Views\AccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Transitions.TransitioningContent TrainsitionigContentSlide;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\MVVM\Views\AccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ReloadPage;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\MVVM\Views\AccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ShowAddingAccountDialog;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\..\MVVM\Views\AccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid RowItemAccounts;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\..\MVVM\Views\AccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoadLastPage;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\..\MVVM\Views\AccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoadNextPage;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\..\..\MVVM\Views\AccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoadPreviousPage;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\..\..\MVVM\Views\AccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoadFirstPage;
        
        #line default
        #line hidden
        
        
        #line 156 "..\..\..\..\MVVM\Views\AccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Expander expander;
        
        #line default
        #line hidden
        
        
        #line 201 "..\..\..\..\MVVM\Views\AccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse ellipse;
        
        #line default
        #line hidden
        
        
        #line 205 "..\..\..\..\MVVM\Views\AccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ImageBrush image;
        
        #line default
        #line hidden
        
        
        #line 215 "..\..\..\..\MVVM\Views\AccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar PBImage;
        
        #line default
        #line hidden
        
        
        #line 243 "..\..\..\..\MVVM\Views\AccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock SelectedAccount_ActivatedString;
        
        #line default
        #line hidden
        
        
        #line 273 "..\..\..\..\MVVM\Views\AccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock SelectedAccount_Id;
        
        #line default
        #line hidden
        
        
        #line 302 "..\..\..\..\MVVM\Views\AccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock SelectedAccount_FullName;
        
        #line default
        #line hidden
        
        
        #line 331 "..\..\..\..\MVVM\Views\AccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock SelectedAccount_Email;
        
        #line default
        #line hidden
        
        
        #line 360 "..\..\..\..\MVVM\Views\AccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock SelectedAccount_Phone;
        
        #line default
        #line hidden
        
        
        #line 388 "..\..\..\..\MVVM\Views\AccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock SelectedAccount_Language;
        
        #line default
        #line hidden
        
        
        #line 417 "..\..\..\..\MVVM\Views\AccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock SelectedAccount_Roles;
        
        #line default
        #line hidden
        
        
        #line 446 "..\..\..\..\MVVM\Views\AccountView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock SelectedPerson_Address;
        
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
            System.Uri resourceLocater = new System.Uri("/WPFBakeryShopAdminV2;component/mvvm/views/accountview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\MVVM\Views\AccountView.xaml"
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
            this.ShowAddingAccountDialog = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.RowItemAccounts = ((System.Windows.Controls.DataGrid)(target));
            
            #line 79 "..\..\..\..\MVVM\Views\AccountView.xaml"
            this.RowItemAccounts.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.RowItemAccounts_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.LoadLastPage = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.LoadNextPage = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.LoadPreviousPage = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.LoadFirstPage = ((System.Windows.Controls.Button)(target));
            return;
            case 9:
            this.expander = ((System.Windows.Controls.Expander)(target));
            return;
            case 10:
            this.ellipse = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 11:
            this.image = ((System.Windows.Media.ImageBrush)(target));
            
            #line 207 "..\..\..\..\MVVM\Views\AccountView.xaml"
            this.image.Changed += new System.EventHandler(this.Image_Changed);
            
            #line default
            #line hidden
            return;
            case 12:
            this.PBImage = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 13:
            this.SelectedAccount_ActivatedString = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 14:
            this.SelectedAccount_Id = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 15:
            this.SelectedAccount_FullName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 16:
            this.SelectedAccount_Email = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 17:
            this.SelectedAccount_Phone = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 18:
            this.SelectedAccount_Language = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 19:
            this.SelectedAccount_Roles = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 20:
            this.SelectedPerson_Address = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

