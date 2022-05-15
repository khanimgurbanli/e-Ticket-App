using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


        public ICollection<MovieOrder> MovieOrders { get; set; }
    }
}
