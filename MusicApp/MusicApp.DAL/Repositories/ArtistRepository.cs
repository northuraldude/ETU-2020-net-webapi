using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicApp.Core.Models;
using MusicApp.Core.Repositories;

namespace MusicApp.DAL.Repositories
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        public ArtistRepository(MusicAppDbContext context) : base(context) { }
        
        public async Task<Artist> GetWithMusicsByIdAsync(int id)
        {
            return await MyMusicDbContext.Artists.Include(a => a.Musics)
                                                 .SingleOrDefaultAsync(a => a.Id == id);
        }
        
        public async Task<IEnumerable<Artist>> GetAllWithMusicsAsync()
        {
            return await MyMusicDbContext.Artists.Include(a => a.Musics)
                                                 .ToListAsync();
        }

        private MusicAppDbContext MyMusicDbContext => Context as MusicAppDbContext;
    }
}