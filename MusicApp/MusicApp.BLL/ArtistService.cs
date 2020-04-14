using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicApp.Core;
using MusicApp.Core.Models;
using MusicApp.Core.Services;

namespace MusicApp.BLL
{
    public class ArtistService : IArtistService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public ArtistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Artist> CreateArtist(Artist newArtist)
        {
            if (IsExist(newArtist))
            {
                await _unitOfWork.Artists.AddAsync(newArtist);
                await _unitOfWork.CommitAsync();
            }
            return newArtist;
        }
        
        public async Task<Artist> GetArtistById(int id)
        {
            return await _unitOfWork.Artists.GetByIdAsync(id);
        }
        
        public async Task<IEnumerable<Artist>> GetAllArtists()
        {
            return await _unitOfWork.Artists.GetAllAsync();
        }

        public async Task UpdateArtist(int id, Artist artist)
        {
            var artistToBeUpdated = await _unitOfWork.Artists.GetWithMusicsByIdAsync(id);
            if (IsExist(artistToBeUpdated))
                artistToBeUpdated.Name = artist.Name;

            await _unitOfWork.CommitAsync();
        }
        
        public async Task DeleteArtist(Artist artist)
        {
            _unitOfWork.Artists.Remove(artist);
            
            await _unitOfWork.CommitAsync();
        }

        private bool IsExist(Artist artist)
        {
            return artist != null;
        }
    }
}