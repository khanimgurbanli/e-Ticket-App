using eTickets.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Actor : IEntityBase
    {
        public int Id { get; set; }
        public string profilPctureUrl { get; set; }
        public string FulName { get; set; }
        public string Bio { get; set; }
        public Movie Movie { get; set; }
        public Producer Producer { get; set; }

        //Relationships
        public ICollection<ActorMovie> ActorMovies { get; set; }
    }
}
