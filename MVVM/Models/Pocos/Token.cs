using Newtonsoft.Json;

namespace WPFBakeryShopAdminV2.MVVM.Models.Pocos
{
    public class Token
    {
        public Token(string idToken)
        {
            IdToken = idToken;
        }

        [JsonProperty("id_token")]
        public string IdToken { get; set; }
    }
}
