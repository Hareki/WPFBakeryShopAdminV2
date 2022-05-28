using System;
using System.Collections.Generic;
using WPFBakeryShopAdminV2.Utilities;

namespace WPFBakeryShopAdminV2.MVVM.Models.Pocos
{
    public class Dashboard
    {
        public int TotalOrdersNum { get; set; }
        public int TodayOrdersNum { get; set; }
        public int TodayProcessingOrdersNum { get; set; }
        public int TodayCancelOrdersNum { get; set; }
        public int TodayDispatchOrdersNum { get; set; }
        public int TodayShippedOrdersNum { get; set; }
        public int TotalAvailableProductVariantsNum { get; set; }
        public int TotalSoldProductVariantsNum { get; set; }
        public int TodaySoldProductVariantsNum { get; set; }
        public List<TopRecentOrders> TopRecentOrders { get; set; }
    }
    public class TopRecentOrders
    {
        public int OrderId { get; set; }
        public string UserImageUrl { get; set; }
        public string UserFullName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int OrderTotal { get; set; }
        public bool PaidByCash { get; set; }
        public string Note { get; set; }
        public string FormattedOrderTotal
        {
            get
            {
                return StringUtils.FormatCurrency(OrderTotal);
            }
        }
    }
}
