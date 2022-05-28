using Newtonsoft.Json;

namespace WPFBakeryShopAdminV2.MVVM.Models.Bodies
{
    public class ChangePasswordBody
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        [JsonIgnore]
        public string ConfirmNewPassword { get; set; }
    }
}
