﻿using System.Collections.Generic;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;

namespace WPFBakeryShopAdminV2.Lists
{
    public class RoleList
    {
        public static List<Role> LIST { get; set; }
        static RoleList()
        {
            LIST = new List<Role>();
            LIST.Add(new Role(AuthRole.ROLE_ADMIN, "Quản trị viên"));
            LIST.Add(new Role(AuthRole.ROLE_USER, "Người dùng"));
        }
    }
}