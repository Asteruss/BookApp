using BookApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Services.AdminService
{
    public interface IChangePubDateService
    {
        ChangePubDateDto GetOriginal(int bookId);
        Book UpdateBook(ChangePubDateDto changedPubDate);

    }
}
