using Microsoft.Win32;
using System.Windows;

namespace WPFBakeryShopAdminV2.Utilities
{
    public class Constants
    {
        public static readonly string PENDING_COLOR = Application.Current.TryFindResource("PendingColor") as string;
        public static readonly string SHIPPING_COLOR = Application.Current.TryFindResource("ShippingColor") as string;
        public static readonly string SHIPPED_COLOR = Application.Current.TryFindResource("ShippedColor") as string;
        public static readonly string CANCELLED_COLOR = Application.Current.TryFindResource("CancelledColor") as string;
        public static readonly string ROLE_ADMIN = "ROLE_ADMIN";
        public static readonly string ROLE_USER = "ROLE_USER";
        public static OpenFileDialog OpenFileDialog
        {
            get
            {
                OpenFileDialog open = new OpenFileDialog
                {
                    Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp",
                    Multiselect = true
                };
                return open;
            }
        }
    }
}
