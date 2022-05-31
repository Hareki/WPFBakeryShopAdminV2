using System.Collections.Generic;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;
using LangStr = WPFBakeryShopAdminV2.Utilities.Language;

namespace WPFBakeryShopAdminV2.Lists
{
    public class RoleList
    {
        public static List<Role> LIST
        {
            get
            {
                List<Role> list = new List<Role>
                {
                    new Role(AuthRole.ROLE_ADMIN,LangStr.Get("Admin")),
                    new Role(AuthRole.ROLE_USER, LangStr.Get("User"))
                };
                return list;
            }
        }
    }

}
