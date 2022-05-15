using eTickets.Data;
using eTickets.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Movie : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        //Relationships
        public ICollection<ActorMovie> ActorMovies { get; set; }
        public ICollection<MovieOrder> MovieOrders { get; set; }

        //Producer
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }

        //Cinema
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }

        //Enum
        public MovieCategory movieCategory { get; set; }
    }
}
