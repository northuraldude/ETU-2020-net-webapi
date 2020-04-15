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
    public class CreateArtistTests
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
            
            artistRepo.Setup(e => e.AddAsync(It.IsAny<Artist>()))
                      .Callback((Artist newArtist) => { dbCollection.Add(newArtist.Id, newArtist); })
                      .Returns((Artist _) => Task.CompletedTask);

            return (unitOfWork, artistRepo, dbCollection);
        }
        
        [Test]
        public async Task CreateArtist_FullInfo_Success()
        {
            // Arrange
            var (unitOfWork, artistRepo, dbCollection) = GetMocks();
            var service = new ArtistService(unitOfWork.Object);
            var artist = new Artist
            {
                Id = 28,
                Name = "New Group"
            };

            // Act
            await service.CreateArtist(artist);

            // Assert
            Assert.IsTrue(dbCollection.ContainsKey(artist.Id));
        }
        
        [Test]
        public void CreateArtist_NullObject_NullReferenceException()
        {
            // Arrange
            var (unitOfWork, artistRepo, dbCollection) = GetMocks();
            var service = new ArtistService(unitOfWork.Object);

            // Act + Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await service.CreateArtist(null));
        }
    }
}