using System;
using System.Threading.Tasks;
using MusicApp.Core.Repositories;

namespace MusicApp.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IMusicRepository Musics { get; }
        IArtistRepository Artists { get; }
        Task<int> CommitAsync();
    }
}