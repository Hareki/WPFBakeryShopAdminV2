namespace WPFBakeryShopAdminV2.MVVM.Models.Pocos
{
    public class Role
    {
        private string _displayName;
        private AuthRole _roleCode;
        public Role(AuthRole roleCode, string displayName)
        {
            this.RoleCode = roleCode;
            this.DisplayName = displayName;
        }
        public AuthRole RoleCode
        {
            get { return _roleCode; }
            set { _roleCode = value; }
        }
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }
        public bool IsLockedRole
        {
            get
            {
                if (this.RoleCode == AuthRole.ROLE_USER)
                {
                    return true;
                }
                return false;
            }
        }

    }
    public enum AuthRole
    {
        ROLE_USER,
        ROLE_ADMIN
    }
}
