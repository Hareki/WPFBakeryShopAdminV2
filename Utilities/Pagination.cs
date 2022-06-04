using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WPFBakeryShopAdminV2.Interfaces;
using LangStr = WPFBakeryShopAdminV2.Utilities.Language;
namespace WPFBakeryShopAdminV2.Utilities
{
    public class Pagination
    {
        public string PageIndicator { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int MaxPageIndex { get; set; }
        public bool CouldLoadFirstPage { get; set; } = false;
        public bool CouldLoadPreviousPage { get; set; } = false;
        public bool CouldLoadNextPage { get; set; } = false;
        public bool CouldLoadLastPage { get; set; } = false;
        public IViewModel ViewModel { get; set; }
        public Pagination(int pageSize, IViewModel viewModel)
        {
            PageSize = pageSize;
            ViewModel = viewModel;
        }
        public void UpdatePaginationStatus(IReadOnlyCollection<RestSharp.HeaderParameter> headers)
        {
            bool linkDone = false, totalCountDone = false;
            foreach (var header in headers)
            {
                if (header.Name.Equals("X-Total-Count"))
                {
                    TotalCount = (Int32.Parse(header.Value.ToString()));
                    MaxPageIndex = TotalCount / PageSize;
                    if (TotalCount % 10 == 0) MaxPageIndex--;
                    UpdatePageIndicator();
                    totalCountDone = true;
                }

                if (header.Name.Equals("Link"))
                {
                    string value = header.Value.ToString();
                    if (value.Contains("next")) CouldLoadNextPage = true;
                    else CouldLoadNextPage = false;

                    if (value.Contains("prev")) CouldLoadPreviousPage = true;
                    else CouldLoadPreviousPage = false;

                    if (value.Contains("first")) CouldLoadFirstPage = true;
                    else CouldLoadFirstPage = false;

                    if (value.Contains("last")) CouldLoadLastPage = true;
                    else CouldLoadLastPage = false;

                    linkDone = true;
                }
                if (totalCountDone && linkDone) return;
            }
        }
        public void UpdatePageIndicator()
        {
            int start = PageSize * CurrentPage + 1;
            int end = CurrentPage == MaxPageIndex ? TotalCount : PageSize * (CurrentPage + 1);
            PageIndicator = $"{start} - {end} {LangStr.Get("of")} {TotalCount}";
        }
        public async Task LoadFirstPage()
        {
            CurrentPage = 0;
            await ViewModel.LoadPageAsync();
        }
        public async Task LoadPreviousPage()
        {
            CurrentPage--;
            await ViewModel.LoadPageAsync();
        }
        public async Task LoadNextPage()
        {
            CurrentPage++;
            await ViewModel.LoadPageAsync();
        }
        public async Task LoadLastPage()
        {
            CurrentPage = MaxPageIndex;
            await ViewModel.LoadPageAsync();
        }
    }
}
