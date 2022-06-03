using Newtonsoft.Json;
using LangStr = WPFBakeryShopAdminV2.Utilities.Language;
namespace WPFBakeryShopAdminV2.MVVM.Models.Pocos
{
    public class ProductVariant
    {
        public ProductVariant()
        {
            this.Id = -1;
        }
        public ProductVariant(int productId)
        {
            this.ProductId = productId;
        }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int TypeId { get; set; }
        public int Cost { get; set; }
        public int Price { get; set; }
        public bool Hot { get; set; }
        public bool Available { get; set; }
        public bool ShouldSerializeId()
        {
            return Id != -1;
        }
        [JsonIgnore]
        public string AvailableName
        {
            get
            {
                if (Available) return LangStr.Get("AvailableStatus");
                else return LangStr.Get("NotAvailableStatus");
            }
        }
        [JsonIgnore]
        public string TypeName => Lists.ProductTypeList.FindTypeNameById(TypeId);
        [JsonIgnore]
        public int TypeIndex => Lists.ProductTypeList.FindTypeIndexById(TypeId);
        [JsonIgnore]
        public string FormattedCost => Utilities.StringUtils.FormatCurrency(Cost);
        [JsonIgnore]
        public string FormattedPrice => Utilities.StringUtils.FormatCurrency(Price);
    }
}
