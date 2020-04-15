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
    public class DeleteMusicTest
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
            
            musicRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                     .ReturnsAsync((int id) => dbCollectionMusic.ContainsKey(id));
            musicRepo.Setup(e => e.Remove(It.IsAny<Music>()))
                     .Callback((Music newMusic) => { dbCollectionMusic.Remove(newMusic.Id); });
            
            artistRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollectionArtists.ContainsKey(id));
            artistRepo.Setup(e => e.Remove(It.IsAny<Artist>()))
                      .Callback((Artist newArtist) => { dbCollectionArtists.Remove(newArtist.Id); });

            return (unitOfWork, musicRepo, dbCollectionMusic);
        }

        [Test]
        public async Task DeleteMusic_TargetItem_Success()
        {
            // Arrange
            var (unitOfWork, musicRepo, dbCollectionMusic) = GetMocks();
            var service = new MusicService(unitOfWork.Object);
            var music = new Music
            {
                Id = 26,
                Name = "Delete Track"
            };

            // Act
            await service.DeleteMusic(music);
            
            // Assert
            Assert.IsFalse(dbCollectionMusic.ContainsKey(26));
        }

        [Test]
        public void DeleteMusic_ItemDoesNotExists_NullReferenceException()
        {
            // Arrange
            var (unitOfWork, musicRepo, dbCollectionMusic) = GetMocks();
            var service = new MusicService(unitOfWork.Object);
            var music = new Music
            {
                Id = 0,
                Name = "Delete Track"
            };

            // Act + Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await service.DeleteMusic(music));
        }
    }
}