using eTickets.Data.Cart;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.ViewComponents
{
    public class SummaryShopping : ViewComponent
    {
        private readonly ShoppingCard _shoppingCard;

        public SummaryShopping(ShoppingCard shoppingCard) => _shoppingCard = shoppingCard;

        public IViewComponentResult Invoke() => View(_shoppingCard.GetAllShoppingCartItems().Count());
    }
}
