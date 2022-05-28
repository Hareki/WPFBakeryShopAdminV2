namespace WPFBakeryShopAdminV2.MVVM.Models.Pocos
{
    public class LanguageItem
    {
        public LanguageItem(string languageName, string languageFlagURL, string langKey)
        {
            LanguageFlagURL = languageFlagURL;
            LanguageName = languageName;
            LangKey = langKey;
        }
        public string LanguageName { get; set; }
        public string LanguageFlagURL { get; set; }
        public string LangKey { get; set; }
    }
}
