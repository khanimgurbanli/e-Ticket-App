using eTickets.Data.Context;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Manager
{
    public class OrderManager : IOrderService
    {
        private readonly AppDBContext _context;
        public OrderManager(AppDBContext context) => _context = context;

        public async Task<List<Order>> GetOrderByUserIdAndRoleAsync(string userId,string userRole) 
        {
            var orders= await _context.Orders.Include(x => x.MovieOrders).ThenInclude(x => x.Movie).Include(x=>x.User).ToListAsync();

            if (userRole!= "Admin")
                orders = orders.Where(o => o.UserId == userId).ToList();
            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderMovie = new MovieOrder()
                {
                    Amount = item.Amount,
                    MovieId = item.MovieId,
                    OrderId = order.Id,
                    Price = item.Movie.Price
                };

                await _context.MovieOrders.AddAsync(orderMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
