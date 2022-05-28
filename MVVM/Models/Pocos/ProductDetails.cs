using Newtonsoft.Json;

namespace WPFBakeryShopAdminV2.MVVM.Models.Pocos
{
    public class ProductDetails
    {
        public ProductDetails()
        {
            this.Id = -1;
        }
        public ProductDetails(ProductDetails another)
        {
            Id = another.Id;
            Name = another.Name;
            CategoryId = another.CategoryId;
            Ingredients = another.Ingredients;
            Allergens = another.Allergens;
            Available = another.Available;
        }
        public int Id { get; set; }
        public bool ShouldSerializeId() { return Id != -1; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Ingredients { get; set; }
        public string Allergens { get; set; }
        public bool Available { get; set; }
        [JsonIgnore]
        public Category Category => Lists.CategoryList.FindCategoryById(CategoryId);

        [JsonIgnore]
        public ProductRowItem ProductRowItem
        {
            get
            {
                return new ProductRowItem(this.Id, this.Name, this.Category.Name, this.Ingredients);
            }
        }
    }
}
