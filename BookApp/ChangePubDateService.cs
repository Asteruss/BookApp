using BookApp.Data.Database;
using BookApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Services.AdminService.Concrete
{
    //ОТКЛЮЧЕННОЕ ОБНОВЛЕНИЕ С ПОВТОРНОЙ ЗАГРУЗКОЙ
    public class ChangePubDateService : IChangePubDateService
    {
        private readonly DBContext _dBContext;
        public ChangePubDateService(DBContext dBContext) =>
            _dBContext = dBContext;
        //????????
        public ChangePubDateDto GetOriginal(int bookId) => _dBContext.Books.Select(b => new ChangePubDateDto()
        {
            BookId = b.BookId,
            PublishedOn = b.PublishedOn,
            Title = b.Title,
        })
        .Single(b => b.BookId == bookId);
        public Book UpdateBook(ChangePubDateDto changePubDateDto)
        {
            Book changedBook = _dBContext.Books.SingleOrDefault(b => b.BookId == changePubDateDto.BookId);
            if (changedBook == null)
                throw new ArgumentException("Книга не найдена");
            changedBook.PublishedOn = changePubDateDto.PublishedOn;
            _dBContext.SaveChanges();
            return changedBook;
        }


    }
}
