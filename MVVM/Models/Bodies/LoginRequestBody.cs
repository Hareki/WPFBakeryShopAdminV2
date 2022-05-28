using Newtonsoft.Json;

namespace WPFBakeryShopAdminV2.MVVM.Models.Bodies
{
    public class LoginRequestBody
    {
        public LoginRequestBody(string email, string password, bool rememberMe)
        {
            Email = email;
            Password = password;
            RememberMe = rememberMe;
        }
        public string Email { get; set; }
        public string Password { get; set; }
        [JsonProperty("remember-me")]
        public bool RememberMe { get; set; }
    }
}
