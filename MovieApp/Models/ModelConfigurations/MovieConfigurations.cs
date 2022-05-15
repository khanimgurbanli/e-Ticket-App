using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models.ModelConfigurations
{
    public class MovieConfigurations : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.BeginDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.ImageUrl).IsRequired();
            builder.HasOne(x => x.Producer).WithMany(x => x.Movies).HasForeignKey(x => x.ProducerId);
            builder.HasOne(x => x.Cinema).WithMany(x => x.Movies).HasForeignKey(x => x.CinemaId);
        }
    }
}
