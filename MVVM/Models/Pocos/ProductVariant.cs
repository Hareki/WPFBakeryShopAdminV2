using Newtonsoft.Json;

namespace WPFBakeryShopAdminV2.MVVM.Models.Pocos
{
    public class ProductVariant
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int TypeId { get; set; }
        public int Cost { get; set; }
        public int Price { get; set; }
        public bool Hot { get; set; }
        public bool Available { get; set; }
        [JsonIgnore]
        public string AvailableName
        {
            get
            {
                if (Available) return "Còn hàng";
                else return "Hết hàng";
            }
        }
        [JsonIgnore]
        public string TypeName => Lists.ProductTypeList.FindTypeNameById(TypeId);
        public string FormattedCost => Utilities.StringUtils.FormatCurrency(Cost);
        public string FormattedPrice => Utilities.StringUtils.FormatCurrency(Price);
    }
}
