using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models.ModelConfigurations
{
    public class ActorMovieConfigurations : IEntityTypeConfiguration<ActorMovie>
    {
        public void Configure(EntityTypeBuilder<ActorMovie> builder)
        {
            builder.HasKey(x => new { x.ActorId, x.MovieId });
            builder.HasOne(x => x.Actor).WithMany(x => x.ActorMovies).HasForeignKey(x => x.ActorId);
            builder.HasOne(x => x.Movie).WithMany(x => x.ActorMovies).HasForeignKey(x => x.MovieId);
        }
    }
}
