using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Services.AdminService
{
    public class ChangePubDateDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime PublishedOn { get; set; }
    }
}
