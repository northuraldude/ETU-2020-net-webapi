using Microsoft.EntityFrameworkCore;
using MusicApp.Core.Models;
using MusicApp.DAL.Configurations;

namespace MusicApp.DAL
{
    public class MusicAppDbContext : DbContext
    {
        public DbSet<Music> Musics { get; set; }
        public DbSet<Artist> Artists { get; set; }

        public MusicAppDbContext(DbContextOptions<MusicAppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MusicConfiguration());
            builder.ApplyConfiguration(new ArtistConfiguration());
        }
    }
}