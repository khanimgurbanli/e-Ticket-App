using eTickets.Data.Context;
using eTickets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Cart
{
    public class ShoppingCard
    {
        private readonly AppDBContext _context;

        public string ShoppinCartId { get; set; }
        public List<ShoppingCartItem> ShoppinCartItems { get; set; }

        public ShoppingCard(AppDBContext context) => _context = context;

        public static ShoppingCard GetShppingCard(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<AppDBContext>();

            string cardId = session.GetString("CardId") ?? Guid.NewGuid().ToString();
            session.SetString("CardId", cardId);

            return new ShoppingCard(context) { ShoppinCartId = cardId };
        }
        public void AddItemToCard(Movie movie)
        {
            var shoppingCardItem = _context.ShoppingCartItems.FirstOrDefault(x => x.Movie.Id == movie.Id && x.ShoppingCartId == ShoppinCartId);

            if (shoppingCardItem== null)
            {
                shoppingCardItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppinCartId,
                    Movie = movie,
                    Amount = 1
                };

                _context.ShoppingCartItems.Add(shoppingCardItem);
            }
            else
            {
                shoppingCardItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCard(Movie movie)
        {
            var shoppingCardItem = _context.ShoppingCartItems.FirstOrDefault(x => x.Movie.Id == movie.Id && x.ShoppingCartId == ShoppinCartId);

            if (shoppingCardItem != null)
            {
                if (shoppingCardItem.Amount>1) shoppingCardItem.Amount--;
                else _context.ShoppingCartItems.Remove(shoppingCardItem);
            }
            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetAllShoppingCartItems()
        {
            return ShoppinCartItems ?? (ShoppinCartItems = _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppinCartId).Include(x => x.Movie).ToList());
        }

        public double ShoppingCartTotal()
        {
            return _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppinCartId).Select(x => x.Movie.Price * x.Amount).Sum();
        }

        public async Task RemoveShoppingCardAsync()
        {
            var removeItem = await _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppinCartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(removeItem);
        }
    }
}
