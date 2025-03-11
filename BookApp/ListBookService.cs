using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookApp.Data.Database;
using BookApp.Services.BookService.Queries;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services.BookService.Concrete
{
    public class ListBookService
    {
        private readonly DBContext _dbContext;
        public ListBookService(DBContext dbContext) => _dbContext = dbContext;
        public IQueryable<BookListDto> SortFilterPage(SortFilterPageOptions options)
        {
            var books = _dbContext.Books.AsNoTracking()
                .MapBookToDto()
                .OrderBooksByOption(options.OrderByOptions)
                .FilterBooksBy(options.FilterOptions, options.FilterValue);
            options.SetupRestOfDTO(books);
            return books.Page(options.PageNum - 1, options.PageSize);
        }
    }
}
