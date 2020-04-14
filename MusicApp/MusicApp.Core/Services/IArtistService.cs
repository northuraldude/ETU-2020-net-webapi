using System.Collections.Generic;
using System.Threading.Tasks;
using MusicApp.Core.Models;

namespace MusicApp.Core.Services
{
    public interface IArtistService
    {
        Task<Artist> CreateArtist(Artist newArtist);
        Task<Artist> GetArtistById(int id);
        Task<IEnumerable<Artist>> GetAllArtists();
        Task UpdateArtist(int id, Artist artist);
        Task DeleteArtist(Artist artist);
    }
}