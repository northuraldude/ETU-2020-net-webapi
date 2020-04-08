using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicApp.Core.Models;
using MusicApp.Core.Repositories;

namespace MusicApp.DAL.Repositories
{
    public class MusicRepository : Repository<Music>, IMusicRepository
    {
        public MusicRepository(MusicAppDbContext context) : base(context) { }

        public async Task<Music> GetWithArtistByIdAsync(int id)
        {
            return await MyMusicDbContext.Musics.Include(m => m.Artist)
                                                .SingleOrDefaultAsync(m => m.Id == id);;
        }
        
        public async Task<IEnumerable<Music>> GetAllWithArtistAsync()
        {
            return await MyMusicDbContext.Musics.Include(m => m.Artist)
                                                .ToListAsync();
        }
        

        public async Task<IEnumerable<Music>> GetAllWithArtistByArtistIdAsync(int artistId)
        {
            return await MyMusicDbContext.Musics.Include(m => m.Artist)
                                                .Where(m => m.ArtistId == artistId)
                                                .ToListAsync();
        }
        
        private MusicAppDbContext MyMusicDbContext => Context as MusicAppDbContext;
    }
}