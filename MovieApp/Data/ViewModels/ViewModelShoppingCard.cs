using eTickets.Data.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.ViewModels
{
    public class ViewModelShoppingCard
    {
        public ShoppingCard ShoppingCard { get; set; }
        public double ShoppingCartTotal { get; set; }

    }
}
