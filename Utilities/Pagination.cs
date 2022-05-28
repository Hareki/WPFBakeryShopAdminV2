using System;
using System.Collections.Generic;
using WPFBakeryShopAdminV2.Interfaces;

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
            PageIndicator = $"{start} - {end} của {TotalCount}";
        }
        public void LoadFirstPage()
        {
            CurrentPage = 0;
            ViewModel.LoadPageAsync();
        }
        public void LoadPreviousPage()
        {
            CurrentPage--;
            ViewModel.LoadPageAsync();
        }
        public void LoadNextPage()
        {
            CurrentPage++;
            ViewModel.LoadPageAsync();
        }
        public void LoadLastPage()
        {
            CurrentPage = MaxPageIndex;
            ViewModel.LoadPageAsync();
        }
    }
}
