using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models.ModelConfigurations
{
    public class MovieOrderConfigurations : IEntityTypeConfiguration<MovieOrder>
    {
        public void Configure(EntityTypeBuilder<MovieOrder> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Amount);
            builder.Property(x => x.Price);
            builder.HasOne(x => x.Order).WithMany(x => x.MovieOrders).HasForeignKey(x => x.OrderId);
            builder.HasOne(x => x.Movie).WithMany(x => x.MovieOrders).HasForeignKey(x => x.MovieId);
        }
    }
}
