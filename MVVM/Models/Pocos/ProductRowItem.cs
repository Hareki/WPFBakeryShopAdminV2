namespace WPFBakeryShopAdminV2.MVVM.Models.Pocos
{
    public class ProductRowItem
    {
        public ProductRowItem(int id, string name, string categoryName, string ingredients)
        {
            Id = id;
            Name = name;
            CategoryName = categoryName;
            Ingredients = ingredients;
        }
        public void SetProperties(ProductRowItem another)
        {
            Id = another.Id;
            Name = another.Name;
            CategoryName = another.CategoryName;
            Ingredients = another.Ingredients;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string Ingredients { get; set; }
    }
}
