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
    public class ArtistServiceTest
    {
        private static (Mock<IUnitOfWork> unitOfWork, Mock<IArtistRepository> artistRepo, Dictionary<int, Artist> dbCollection) GetMocks()
        {
            var unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            var artistRepo = new Mock<IArtistRepository>(MockBehavior.Strict);

            var dbCollection = new Dictionary<int, Artist>
            {
                [27] = new Artist
                {
                    Id = 27,
                    Name = "Old Group"
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
        public async Task CreateArtist_Success()
        {
            var (unitOfWork, artistRepo, dbCollection) = GetMocks();
            artistRepo.Setup(e => e.AddAsync(It.IsAny<Artist>()))
                      .Callback((Artist newArtist) => { dbCollection.Add(28, newArtist); })
                      .Returns((Artist _) => Task.CompletedTask);
            
            var service = new ArtistService(unitOfWork.Object);
            
            var artist = new Artist
            {
                Name = "New Group"
            };

            await service.CreateArtist(artist);

            Assert.IsTrue(dbCollection.ContainsKey(28));
        }
        
        [Test]
        public async Task UpdateArtist_Success()
        {
            var (unitOfWork, artistRepo, dbCollection)  = GetMocks();
            var service = new ArtistService(unitOfWork.Object);
            
            var artist = new Artist
            {
                Name = "New Group"
            };
        
            await service.UpdateArtist(27, artist);
            
            Assert.AreEqual((await unitOfWork.Object.Artists.GetByIdAsync(27)).Name, artist.Name);
        }
        
        [Test]
        public async Task UpdateArtist_EmptyName_NotUpdated()
        {
            var (unitOfWork, artistRepo, dbCollection)  = GetMocks();
            var service = new ArtistService(unitOfWork.Object);

            var artist = new Artist()
            {
                Name = ""
            };

            await service.UpdateArtist(27, artist);
        
            Assert.IsNotNull((await unitOfWork.Object.Artists.GetByIdAsync(27)).Name);
        }
    }
}