using MusicApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MusicApp.DAL.Configurations
{
    public class MusicConfiguration : IEntityTypeConfiguration<Music>
    {
        public void Configure(EntityTypeBuilder<Music> builder)
        {
            const int maxLength = 50;
            
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                   .UseIdentityColumn();
                
            builder.Property(m => m.Name)
                   .IsRequired()
                   .HasMaxLength(maxLength);

            builder.HasOne(m => m.Artist)
                   .WithMany(a => a.Musics)
                   .HasForeignKey(m => m.ArtistId);

            builder.ToTable("Musics");
        }
    }
}