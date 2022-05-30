using System;
using System.Collections.Generic;
using System.Windows;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;


namespace WPFBakeryShopAdminV2.Utilities
{
    internal class Language
    {
        private static ResourceDictionary _enlishDictionary;
        private static ResourceDictionary _vnDictionary;
        static Language()
        {
            _enlishDictionary = new ResourceDictionary();
            _vnDictionary = new ResourceDictionary();

            _enlishDictionary.Source = new Uri("..\\Resources\\Languages\\StringResources.en.xaml", UriKind.Relative);
            _vnDictionary.Source = new Uri("..\\Resources\\Languages\\StringResources.vn.xaml", UriKind.Relative);
        }
        public static void SwitchLanguage(int selectedIndex)
        {
            Application.Current.Resources.MergedDictionaries.Remove(_enlishDictionary);
            Application.Current.Resources.MergedDictionaries.Remove(_vnDictionary);
            switch (selectedIndex)
            {
                case 0:
                    Application.Current.Resources.MergedDictionaries.Add(_vnDictionary);

                    break;
                case 1:
                    Application.Current.Resources.MergedDictionaries.Add(_enlishDictionary);
                    break;
                default:
                    Application.Current.Resources.MergedDictionaries.Add(_vnDictionary);
                    break;
            }
        }
        public static string Get(string key)
        {
            return Application.Current.Resources[key].ToString();
        }
    }
}
