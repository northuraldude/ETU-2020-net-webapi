using System.Collections.Generic;
using System.Threading.Tasks;
using MusicApp.Core.Models;

namespace MusicApp.Core.Repositories
{
    public interface IArtistRepository : IRepository<Artist>
    {
        Task<Artist> GetWithMusicsByIdAsync(int id);
        Task<IEnumerable<Artist>> GetAllWithMusicsAsync();
    }
}