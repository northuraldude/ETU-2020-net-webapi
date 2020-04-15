using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Moq;
using MusicApp.Core;
using MusicApp.Core.Models;
using MusicApp.Core.Repositories;
using NUnit.Framework;

namespace MusicApp.BLL.Tests
{
    [TestFixture]
    public class UpdateMusicTests
    {
        private static (Mock<IUnitOfWork> unitOfWork, Mock<IMusicRepository> musicRepo, Dictionary<int, Music> dbCollectionMusic) GetMocks()
        {
            var unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            var musicRepo = new Mock<IMusicRepository>(MockBehavior.Strict);
            var artistRepo = new Mock<IArtistRepository>(MockBehavior.Strict);
            var dbCollectionMusic = new Dictionary<int, Music>
            {
                [26] = new Music
                {
                    Id = 26,
                    ArtistId = 26,
                    Name = "Delete Track"
                },
                [27] = new Music
                {
                    Id = 27,
                    ArtistId = 27,
                    Name = "Track"
                }
            };
            
            var dbCollectionArtists = new Dictionary<int, Artist>
            {
                [26] = new Artist
                {
                    Id = 26,
                    Name = "Group"
                },
                [27] = new Artist
                {
                    Id = 27,
                    Name = "Other Group"
                }
            };

            unitOfWork.SetupGet(e => e.Musics).Returns(musicRepo.Object);
            unitOfWork.SetupGet(e => e.Artists).Returns(artistRepo.Object);
            unitOfWork.Setup(e => e.CommitAsync()).ReturnsAsync(0);
            
            musicRepo.Setup(e => e.GetWithArtistByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollectionMusic[id]);
            musicRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollectionMusic.ContainsKey(id));
            
            artistRepo.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollectionArtists[id]);
            artistRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollectionArtists.ContainsKey(id));

            return (unitOfWork, musicRepo, dbCollectionMusic);
        }
        
        [Test]
        public async Task UpdateMusic_FullInfo_Success()
        {
            // Arrange
            var (unitOfWork, musicRepo, dbCollectionMusic)  = GetMocks();
            var service = new MusicService(unitOfWork.Object);
            var music = new Music
            {
                ArtistId = 27,
                Name = "New Track"
            };
        
            // Act
            await service.UpdateMusic(27, music);
            
            // Assert
            Assert.AreEqual((await unitOfWork.Object.Musics.GetWithArtistByIdAsync(27)).Name, music.Name);
        }
        
        [Test]
        public void UpdateMusic_EmptyName_InvalidDataException()
        {
            // Arrange
            var (unitOfWork, musicRepo, dbCollectionMusic)  = GetMocks();
            var service = new MusicService(unitOfWork.Object);
            var music = new Music()
            {
                Name = ""
            };
            
            // Act + Assert
            Assert.ThrowsAsync<InvalidDataException>(async () => await service.UpdateMusic(27, music));
        }
        
        [Test]
        public void UpdateMusic_NoItemForUpdate_NullReferenceException()
        {
            // Arrange
            var (unitOfWork, musicRepo, dbCollectionMusic)  = GetMocks();
            var service = new MusicService(unitOfWork.Object);
            var music = new Music()
            {
                Name = "Update Track"
            };
            
            // Act + Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await service.UpdateMusic(0, music));
        }
    }
}