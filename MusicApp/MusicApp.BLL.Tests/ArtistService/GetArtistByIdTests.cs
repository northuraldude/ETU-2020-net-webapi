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
    public class GetArtistByIdTests
    {
        private static (Mock<IUnitOfWork> unitOfWork, Mock<IArtistRepository> artistRepo, Dictionary<int, Artist> dbCollection) GetMocks()
        {
            var unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            var artistRepo = new Mock<IArtistRepository>(MockBehavior.Strict);
            var dbCollection = new Dictionary<int, Artist>
            {
                [26] = new Artist
                {
                    Id = 26,
                    Name = "Delete Group"
                },
                [27] = new Artist
                {
                    Id = 27,
                    Name = "Group"
                }
            };

            unitOfWork.SetupGet(e => e.Artists).Returns(artistRepo.Object);
            unitOfWork.Setup(e => e.CommitAsync()).ReturnsAsync(0);
            
            artistRepo.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollection[id]);

            return (unitOfWork, artistRepo, dbCollection);
        }
        
        [Test]
        public async Task GetArtistById_ItemExists_Success()
        {
            // Arrange
            var (unitOfWork, artistRepo, dbCollection) = GetMocks();
            var service = new ArtistService(unitOfWork.Object);

            // Act
            var artist = await service.GetArtistById(27);
            
            // Assert
            Assert.AreEqual(artist, dbCollection[27]);
        }
        
        [Test]
        public void GetArtistById_ItemDoesNotExists_KeyNotFoundException()
        {
            // Arrange
            var (unitOfWork, artistRepo, dbCollection) = GetMocks();
            var service = new ArtistService(unitOfWork.Object);

            // Act + Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await service.GetArtistById(0));
        }
    }
}