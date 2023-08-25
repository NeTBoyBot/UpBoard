using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Board.Infrastucture.DataAccess.Contexts.User
{
    public class UserConfiguration : IEntityTypeConfiguration<UpBoard.Domain.User>
    {
        public void Configure(EntityTypeBuilder<UpBoard.Domain.User> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Username).HasMaxLength(256).IsRequired();
            builder.Property(a => a.Password).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Registrationdate).HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));
            
            builder.HasMany(u => u.FavoriteAds).WithOne(f => f.User).HasForeignKey(f => f.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u=>u.Advertisements).WithOne(a=>a.Owner).HasForeignKey(f=>f.OwnerId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u=>u.RecievedComments).WithOne(c=>c.User).HasForeignKey(f=>f.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u=>u.SendedComments).WithOne(c=>c.Sender).HasForeignKey(f=>f.SenderId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
