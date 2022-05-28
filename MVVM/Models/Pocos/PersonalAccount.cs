using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace WPFBakeryShopAdminV2.MVVM.Models.Pocos
{
    public class PersonalAccount
    {
        public PersonalAccount()
        {
            this.LangKey = "vi";
            this.Authorities = new List<string>();
        }
        public PersonalAccount(PersonalAccount another)
        {
            this.FirstName = another.FirstName;
            this.LastName = another.LastName;
            this.Email = another.Email;
            this.Phone = another.Phone;
            this.Authorities = another.Authorities;
            this.ImageUrl = another.ImageUrl;
            this.Address = another.Address;
            this.LangKey = another.LangKey;
            this.Activated = another.Activated;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string LangKey { get; set; }

        public string ImageUrl { get; set; }
        public bool ShouldSerializeImageUrl() { return false; }
        public bool Activated { get; set; }
        public bool ShouldSerializeActivated() { return false; }
        public List<string> Authorities { get; set; }
        public bool ShouldSerializeAuthorities() { return SerializeAuth; }
        [JsonIgnore]
        public bool SerializeAuth { get; set; }

        [JsonIgnore]
        public string LanguageName
        {
            get
            {
                switch (LangKey)
                {
                    case "en": return "English";
                    case "vi": return "Việt Nam";
                    default: return "ERROR";
                }
            }
        }

        [JsonIgnore]
        public int LanguageIndex
        {
            set
            {
                switch (value)
                {
                    case 0: LangKey = "vi"; break;
                    case 1: LangKey = "en"; break;
                    default: Debug.Assert(false); break;
                }
            }
            get
            {
                switch (LangKey)
                {
                    case "vi": return 0;
                    case "en": return 1;
                    default: Debug.Assert(false); return -1;
                }
            }
        }

        [JsonIgnore]
        public string FullName
        {
            get
            {
                return $"{LastName} {FirstName}";
            }
        }
    }
}
