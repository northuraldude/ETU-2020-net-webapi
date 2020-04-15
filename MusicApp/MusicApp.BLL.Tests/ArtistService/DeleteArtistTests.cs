using System;
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
    public class DeleteArtistTests
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
            
            artistRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollection.ContainsKey(id));
            artistRepo.Setup(e => e.Remove(It.IsAny<Artist>()))
                      .Callback((Artist newArtist) => { dbCollection.Remove(newArtist.Id); });

            return (unitOfWork, artistRepo, dbCollection);
        }

        [Test]
        public async Task DeleteArtist_TargetItem_Success()
        {
            // Arrange
            var (unitOfWork, artistRepo, dbCollection) = GetMocks();
            var service = new ArtistService(unitOfWork.Object);
            var artist = new Artist
            {
                Id = 26,
                Name = "Delete Group"
            };

            // Act
            await service.DeleteArtist(artist);
            
            // Assert
            Assert.IsFalse(dbCollection.ContainsKey(26));
        }

        [Test]
        public void DeleteArtist_ItemDoesNotExists_NullReferenceException()
        {
            // Arrange
            var (unitOfWork, artistRepo, dbCollection) = GetMocks();
            var service = new ArtistService(unitOfWork.Object);
            var artist = new Artist
            {
                Id = 0,
                Name = "Delete Group"
            };

            // Act + Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await service.DeleteArtist(artist));
        }
    }
}