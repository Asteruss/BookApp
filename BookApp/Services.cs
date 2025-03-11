using BookApp.Data.Models;
using BookApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Services.BookService.Queries
{
    public static class BookListDtoExtension
    {
        public const string AllBooksNotPublishedString = "Coming Soon";
        public static IQueryable<BookListDto> MapBookToDto(this IQueryable<Book> books) =>
            books.Select(book => new BookListDto()
            {
                BookId = book.BookId,
                Title = book.Title,
                PublishedOn = book.PublishedOn,
                Price = book.Price,
                ActualPrice = (book.PriceOffer == null) ? book.Price : book.PriceOffer.NewPrice,
                PromotionPromotionalText = (book.PriceOffer == null) ? "" : book.PriceOffer.PromotionText,
                AuthorsOrdered = string.Join(", ", book.Authors.OrderBy(auth => auth.Order).Select(auth => auth.Author).Select(a => a.Name).ToList()),
                ReviewsCount = book.Reviews.Count,
                ReviewsAverageVotes = book.Reviews.Select(r => r.Stars).Average(),
                TagStrings = book.Tags.Select(t => t.TagId).ToArray()
            });
        public static IQueryable<BookListDto> OrderBooksByOption(this IQueryable<BookListDto> books, OrderByOptions options) => options switch
        {
            OrderByOptions.SimpleOrder => books.OrderByDescending(b => b.BookId),
            OrderByOptions.ByVotes => books.OrderByDescending(b => b.ReviewsAverageVotes),
            OrderByOptions.ByPublicationDate => books.OrderByDescending(b => b.PublishedOn),
            OrderByOptions.ByPriceHighestFirst => books.OrderByDescending(b => b.ActualPrice),
            OrderByOptions.ByPriceLowestFirst => books.OrderBy(b => b.ActualPrice),
            _ => throw new ArgumentOutOfRangeException(nameof(options), options, null)

        };
        public static IQueryable<BookListDto> FilterBooksBy(this IQueryable<BookListDto> books, FilterOptions options, string filerValue) =>
            string.IsNullOrWhiteSpace(filerValue) ? books : options switch
            {
                FilterOptions.NoFilter => books,
                FilterOptions.ByVotes => books.Where(b => b.ReviewsAverageVotes > int.Parse(filerValue)),
                FilterOptions.ByTags => books.Where(b => b.TagStrings.Any(t => t == filerValue)),
                FilterOptions.ByPublicationYear => (filerValue == AllBooksNotPublishedString)? books.Where(b => b.PublishedOn > DateTime.UtcNow) :
                    books.Where(b => b.PublishedOn.Year == int.Parse(filerValue) && b.PublishedOn < DateTime.UtcNow),
                _ => throw new ArgumentOutOfRangeException(nameof(options), options, null)

            };
        public static IQueryable<T> Page<T>(this IQueryable<T> query, int pageNumZeroStart, int pageSize)
        {
            if (pageSize == 0) throw new ArgumentOutOfRangeException(nameof(pageSize), "Размер страницы не может быть равен нулю");
            if (pageNumZeroStart != 0) 
                query = query.Skip(pageSize * pageNumZeroStart);
            return query.Take(pageSize);
        }
    }

}
