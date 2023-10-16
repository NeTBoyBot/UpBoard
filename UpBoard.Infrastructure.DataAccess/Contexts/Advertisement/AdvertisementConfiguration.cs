using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpBoard.Infrastructure.DataAccess.Contexts.Advertisement
{
    public class AdvertisementConfiguration : IEntityTypeConfiguration<Domain.Advertisement>
    {
        public void Configure(EntityTypeBuilder<Domain.Advertisement> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a=>a.Id).ValueGeneratedOnAdd();
            builder.Property(a=>a.Description).HasMaxLength(256).IsRequired();
            builder.Property(a => a.Name).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Price).IsRequired();
            builder.HasOne(ad => ad.Category).WithMany(c => c.Advertisements).HasForeignKey(c => c.Id);
        }
    }
}
