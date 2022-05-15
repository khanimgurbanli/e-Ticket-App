using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IOrderService _orderService;
        private readonly ShoppingCard _shoppingCard;

        public OrdersController(IMovieService movieService, ShoppingCard shoppingCard, IOrderService orderService)
        {
            _movieService = movieService;
            _orderService = orderService;
            _shoppingCard = shoppingCard;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            return View( await _orderService.GetOrderByUserIdAndRoleAsync(userId,userRole));
        }


        public IActionResult ShoppingCard()
        {
            var returnData = _shoppingCard.GetAllShoppingCartItems();

            _shoppingCard.ShoppinCartItems = returnData;

            var response = new ViewModelShoppingCard()
            {
                ShoppingCard = _shoppingCard,
                ShoppingCartTotal=_shoppingCard.ShoppingCartTotal() 
            };

            return View(response);
        }

       
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _movieService.GetMovieByIdAsync(id);

            if (item !=null)
            {
                _shoppingCard.AddItemToCard(item);
            }
            return RedirectToAction(nameof(ShoppingCard));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _movieService.GetMovieByIdAsync(id);

            if (item != null)
            {
                _shoppingCard.RemoveItemFromCard(item);
            }
            return RedirectToAction(nameof(ShoppingCard));
        }


        public async Task<IActionResult> CompleteOrder()
        {
            var shoppingCardItems = _shoppingCard.GetAllShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _orderService.StoreOrderAsync(shoppingCardItems, userId,userEmailAddress);
            await _shoppingCard.RemoveShoppingCardAsync();

            return View("CompletedOrder");
        }

    }
}
