using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicApp.Core.Models;

namespace MusicApp.DAL.Configurations
{
    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            const int maxLength = 50;
            
            builder.HasKey(a => a.Id);

            builder.Property(m => m.Id)
                   .UseIdentityColumn();
                
            builder.Property(m => m.Name)
                   .IsRequired()
                   .HasMaxLength(maxLength);

            builder.ToTable("Artists");
        }
    }
}