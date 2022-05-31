using System;
using WPFBakeryShopAdminV2.Utilities;
using LangStr = WPFBakeryShopAdminV2.Utilities.Language;

namespace WPFBakeryShopAdminV2.MVVM.Models.Pocos
{
    public class BillRowItem
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int StatusId { get; set; }
        public int Total { get; set; }
        public string CustomerName { get; set; }
        public string StatusString
        {
            get
            {
                switch (StatusId)
                {
                    case 1: return LangStr.Get("Pending");
                    case 2: return LangStr.Get("Shipping");
                    case 3: return LangStr.Get("Shipped");
                    case 4: return LangStr.Get("Cancelled");
                    default:
                        {
                            Console.WriteLine("ID: " + StatusId);
                            return "Trạng thái không hợp lệ";
                        }
                }
            }
        }
        public string FormattedTotal
        {
            get
            { return StringUtils.FormatCurrency(Total); }
        }
    }
}
