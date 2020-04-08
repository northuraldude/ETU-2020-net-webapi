using System.Threading.Tasks;
using Moq;
using MusicApp.Core;
using MusicApp.Core.Models;
using NUnit.Framework;

namespace MusicApp.BLL.Tests
{
    [TestFixture]
    public class MusicServiceTest
    {
        private Mock<IUnitOfWork> _unitOfWork;

        [SetUp]
        public void Init()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Test]
        public async Task UpdateMusic_Success()
        {
            var service = new MusicService(_unitOfWork.Object);
            var music = new Music
            {
                Name = "New Music"
            };

            var musicToBeUpdated = new Music
            {
                Name = "Old Music"
            };

            await service.UpdateMusic(musicToBeUpdated, music);
            
            Assert.AreEqual(musicToBeUpdated.Name, music.Name);
        }

        [Test]
        public void UpdateMusic_NoName_NotUpdated()
        {
            var service = new MusicService(_unitOfWork.Object);
            var music = new Music();
            var musicToBeUpdated = new Music
            {
                Name = "Music"
            };

            service.UpdateMusic(musicToBeUpdated, music);

            Assert.IsNotNull(musicToBeUpdated.Name);
        }
    }
}