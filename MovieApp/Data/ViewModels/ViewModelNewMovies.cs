using eTickets.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.ViewModels
{
    public class ViewModelNewMovies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        //Relationships
        public ICollection<int> ActorIds { get; set; }

        public int ProducerId { get; set; }
        public int CinemaId { get; set; }
        //Enum
        public MovieCategory movieCategory { get; set; }
    }
}
