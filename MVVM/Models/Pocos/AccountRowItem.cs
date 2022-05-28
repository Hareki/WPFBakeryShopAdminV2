using System.Collections.Generic;
using System.Text;

namespace WPFBakeryShopAdminV2.MVVM.Models.Pocos
{
    public class AccountRowItem
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
        public string Address { get; set; }
        public bool Activated { get; set; }
        public string LangKey { get; set; }
        public List<string> Authorities { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
        public string ActivatedString
        {
            get { return Activated ? "Đã kích hoạt" : "Chưa kích hoạt"; }
        }
        public string ActivatedColor
        {
            get { return Activated ? "MediumSeaGreen" : "Red"; }
        }
        public string Language
        {
            get
            {
                if (string.IsNullOrEmpty(LangKey)) return "";
                else if (LangKey.Equals("en")) return "English";
                else return "Tiếng Việt";

            }
        }
        public string Roles
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in Authorities)
                {
                    if (item.Equals("ROLE_USER"))
                    {
                        sb.Append("User, ");
                    }
                    else
                    {
                        sb.Append("Admin, ");
                    }

                }
                if (sb.Length > 2)
                {
                    sb.Remove(sb.Length - 2, 2);
                }
                return sb.ToString();
            }
        }

    }
}
