using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models.ModelConfigurations
{
    public class ProducerConfigurations : IEntityTypeConfiguration<Producer>
    {
        public void Configure(EntityTypeBuilder<Producer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.FulName).IsRequired().HasMaxLength(80);
            builder.Property(x => x.profilPctureUrl).IsRequired();
            builder.Property(x => x.Bio).IsRequired();
        }
    }

}
