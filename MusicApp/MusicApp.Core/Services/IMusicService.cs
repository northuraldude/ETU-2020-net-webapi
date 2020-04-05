using System.Collections.Generic;
using System.Threading.Tasks;
using MusicApp.Core.Models;

namespace MusicApp.Core.Services
{
    public interface IMusicService
    {
        Task<Music> CreateMusic(Music newMusic);
        Task<Music> GetMusicById(int id);
        Task<IEnumerable<Music>> GetAllWithArtist();
        Task<IEnumerable<Music>> GetMusicsByArtistId(int artistId);
        Task UpdateMusic(Music musicToBeUpdated, Music music);
        Task DeleteMusic(Music music);
    }
}