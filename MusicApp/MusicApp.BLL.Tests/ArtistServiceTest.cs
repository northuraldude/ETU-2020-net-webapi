using System.Threading.Tasks;
using Moq;
using MusicApp.Core;
using MusicApp.Core.Models;
using NUnit.Framework;

namespace MusicApp.BLL.Tests
{
    [TestFixture]
    public class ArtistServiceTest
    {
        private Mock<IUnitOfWork> _unitOfWork;

        [SetUp]
        public void Init()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Test]
        public async Task UpdateArtist_Success()
        {
            var service = new ArtistService(_unitOfWork.Object);
            var artist = new Artist
            {
                Name = "New Group"
            };

            var artistToBeUpdated = new Artist
            {
                Name = "Old group"
            };

            await service.UpdateArtist(artistToBeUpdated, artist);
            
            Assert.AreEqual(artistToBeUpdated.Name, artist.Name);
        }

        [Test]
        public void UpdateArtist_NewInformationDoesntExist_NotUpdated()
        {
            var service = new ArtistService(_unitOfWork.Object);
            var artist = new Artist();
            var artistToBeUpdated = new Artist
            {
                Name = "Group"
            };

            service.UpdateArtist(artistToBeUpdated, artist);

            Assert.IsNotNull(artistToBeUpdated.Name);
        }
    }
}