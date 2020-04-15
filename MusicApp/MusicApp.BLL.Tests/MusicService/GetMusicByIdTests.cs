using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MusicApp.Core;
using MusicApp.Core.Models;
using MusicApp.Core.Repositories;
using NUnit.Framework;

namespace MusicApp.BLL.Tests
{
    [TestFixture]
    public class GetMusicByIdTests
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
            
            artistRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollectionArtists.ContainsKey(id));

            return (unitOfWork, musicRepo, dbCollectionMusic);
        }
        
        [Test]
        public async Task GetMusicById_ItemExists_Success()
        {
            // Arrange
            var (unitOfWork, musicRepo, dbCollectionMusic) = GetMocks();
            var service = new MusicService(unitOfWork.Object);

            // Act
            var music = await service.GetMusicById(27);
            
            // Assert
            Assert.AreEqual(music, dbCollectionMusic[27]);
        }
        
        [Test]
        public void GetMusicById_ItemDoesNotExists_KeyNotFoundException()
        {
            // Arrange
            var (unitOfWork, musicRepo, dbCollectionMusic) = GetMocks();
            var service = new MusicService(unitOfWork.Object);

            // Act + Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await service.GetMusicById(0));
        }
    }
}