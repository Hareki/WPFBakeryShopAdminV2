using System;
using WPFBakeryShopAdminV2.Utilities;

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
                    case 1: return "Chờ duyệt";
                    case 2: return "Đang giao";
                    case 3: return "Đã giao";
                    case 4: return "Đã hủy";
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
