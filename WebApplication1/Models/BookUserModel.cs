using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarrowBookApp.Models
{
    public class BookUserModel
    {
        public catalogBook Book { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
    }
}
