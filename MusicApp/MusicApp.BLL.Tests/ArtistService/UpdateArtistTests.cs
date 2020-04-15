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
    public class UpdateArtistTests
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
            artistRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollection.ContainsKey(id));

            return (unitOfWork, artistRepo, dbCollection);
        }
        
        [Test]
        public async Task UpdateArtist_FullInfo_Success()
        {
            // Arrange
            var (unitOfWork, artistRepo, dbCollection)  = GetMocks();
            var service = new ArtistService(unitOfWork.Object);
            var artist = new Artist
            {
                Name = "New Group"
            };
        
            // Act
            await service.UpdateArtist(27, artist);
            
            // Assert
            Assert.AreEqual((await unitOfWork.Object.Artists.GetByIdAsync(27)).Name, artist.Name);
        }
        
        [Test]
        public void UpdateArtist_EmptyName_InvalidDataException()
        {
            // Arrange
            var (unitOfWork, artistRepo, dbCollection)  = GetMocks();
            var service = new ArtistService(unitOfWork.Object);
            var artist = new Artist()
            {
                Name = ""
            };
            
            // Act + Assert
            Assert.ThrowsAsync<InvalidDataException>(async () => await service.UpdateArtist(27, artist));
        }
        
        [Test]
        public void UpdateArtist_NoItemForUpdate_NullReferenceException()
        {
            // Arrange
            var (unitOfWork, artistRepo, dbCollection)  = GetMocks();
            var service = new ArtistService(unitOfWork.Object);
            var artist = new Artist()
            {
                Name = "Update Group"
            };
            
            // Act + Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await service.UpdateArtist(0, artist));
        }
    }
}