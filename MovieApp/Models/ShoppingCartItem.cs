using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
