using Newtonsoft.Json;

namespace WPFBakeryShopAdminV2.MVVM.Models.Pocos
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }

        [JsonIgnore]
        public bool IsSelected { get; set; }
    }
}
