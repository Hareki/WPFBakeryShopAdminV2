using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using WPFBakeryShopAdminV2.MVVM.Models.Pocos;
using WPFBakeryShopAdminV2.Utilities;

namespace WPFBakeryShopAdminV2.Lists
{
    public class CategoryList
    {
        static List<Category> _categories = null;
        private static readonly RestClient _restClient;
        public static bool Loading { get; set; }
        static CategoryList()
        {
            _restClient = RestConnection.PublicRestClient;
        }
        public static async Task<List<Category>> LoadCategoryList()
        {
            Loading = true;
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Get, "categories", null, null);

            if ((int)response.StatusCode == 200)
            {
                var categories = response.Content;
                _categories = JsonConvert.DeserializeObject<List<Category>>(categories);
            }
            Loading = false;
            return _categories;
        }
        public static Category FindCategoryById(int id)
        {
            if (_categories != null)
            {
                Category result = _categories.Find((element) => element.Id == id);
                return result;
            }
            return null;
        }
    }
}
