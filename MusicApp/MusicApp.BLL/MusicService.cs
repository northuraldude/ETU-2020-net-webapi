using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicApp.Core;
using MusicApp.Core.Models;
using MusicApp.Core.Services;

namespace MusicApp.BLL
{
    public class MusicService : IMusicService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public MusicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Music> CreateMusic(Music newMusic)
        {
            if (newMusic is null)
                throw new NullReferenceException();
            
            await _unitOfWork.Musics.AddAsync(newMusic);
            await _unitOfWork.CommitAsync();

            return newMusic;
        }

        public async Task<Music> GetMusicById(int id)
        {
            return await _unitOfWork.Musics.GetWithArtistByIdAsync(id);
        }

        public async Task<IEnumerable<Music>> GetMusicsByArtistId(int artistId)
        {
            return await _unitOfWork.Musics.GetAllWithArtistByArtistIdAsync(artistId);
        }

        public async Task<IEnumerable<Music>> GetAllWithArtist()
        {
            return await _unitOfWork.Musics.GetAllWithArtistAsync();
        }

        public async Task UpdateMusic(int id, Music music)
        {
            if (!await _unitOfWork.Musics.IsExists(id))
                throw new NullReferenceException();
            
            var musicToBeUpdated = await GetMusicById(id);
            
            musicToBeUpdated.Name = music.Name;
            musicToBeUpdated.ArtistId = music.ArtistId;

            await _unitOfWork.CommitAsync();
        }
        
        public async Task DeleteMusic(Music music)
        {
            _unitOfWork.Musics.Remove(music);
            
            await _unitOfWork.CommitAsync();
        }
    }
}