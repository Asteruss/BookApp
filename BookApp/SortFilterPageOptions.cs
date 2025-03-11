using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookApp.Data.Models;

namespace BookApp.Services.BookService
{
    public class SortFilterPageOptions
    {
        public const int DefaultPageSize = 10;
        public OrderByOptions OrderByOptions { get; set; }
        public FilterOptions FilterOptions { get; set; }
        public string FilterValue { get; set; }

        public int[] PageSizes = { 5, DefaultPageSize, 20, 50, 100, 200 };
        private int _pageNum = 1;

        private int _pageSize = DefaultPageSize;
        public int PageNum
        {
            get { return _pageNum; }
            set { _pageNum = value; }
        }
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
        public int NumPages { get; private set; }
        public string PrevCheckState { get; set; }
        public void SetupRestOfDTO<T>(IQueryable<T> query)
        {
            NumPages = (int)Math.Ceiling((double)query.Count() / PageSize);
            PageNum = Math.Min(Math.Max(1, PageNum), NumPages);
            var newCheck = GenerateCheckString();
            if (newCheck != PrevCheckState)
                PageNum = 1;
            PrevCheckState = newCheck;
        }
        private string GenerateCheckString() => $"{(int)FilterOptions}{FilterValue}{PageSize}{NumPages}";
    }
}
