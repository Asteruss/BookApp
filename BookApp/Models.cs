using System.ComponentModel.DataAnnotations;

namespace BookApp.Data.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedOn { get; set; }
        public string Publisher { get; set; }
        public decimal Price { get; set; }

        public PriceOffer PriceOffer { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<BookAuthor> Authors { get; set; }
        public ICollection<Tag> Tags { get; set; }

    }
    public class PriceOffer
    {
        public int PriceOfferId { get; set; }
        public decimal NewPrice { get; set; }
        public string PromotionText { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
    public class Review
    {
        public int ReviewId { get; set; }
        public string Comment { get; set; }
        public int Stars { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }

    }
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
    public class BookAuthor
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public int Order { get; set; }
        public Book Book { get; set; } //#J
        public Author Author { get; set; } //#K
    }
    public class Tag
    {
        public string TagId { get; set; }
        public ICollection<Book> Books { get; set; }
    }
    public enum OrderByOptions
    {
        [Display(Name = "sort by...")] SimpleOrder,
        [Display(Name = "Votes ↑")] ByVotes,
        [Display(Name = "Publication Date ↑")] ByPublicationDate,
        [Display(Name = "Price ↓")] ByPriceLowestFirst,
        [Display(Name = "Price ↑")] ByPriceHighestFirst
    }
    public enum FilterOptions
    {
        [Display(Name = "All")] NoFilter,
        [Display(Name = "By Votes...")] ByVotes,
        [Display(Name = "By Categories...")] ByTags,
        [Display(Name = "By Year published...")] ByPublicationYear
    }
}
