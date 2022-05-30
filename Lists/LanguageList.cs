using System;
using System.Collections.Generic;
using System.Windows;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;

namespace WPFBakeryShopAdminV2.Lists
{
    public class LanguageList
    {
        public static List<LanguageItem> LIST { get; set; }
        static LanguageList()
        {
            LIST = new List<LanguageItem>();
            LIST.Add(new LanguageItem("Tiếng Việt", "/Resources/Images/vn-flag.png", "vi"));
            LIST.Add(new LanguageItem("English", "/Resources/Images/uk-flag.png", "en"));
        }
    }
}
