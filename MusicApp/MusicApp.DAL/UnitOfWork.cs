using System.Threading.Tasks;
using MusicApp.Core;
using MusicApp.Core.Repositories;
using MusicApp.DAL.Repositories;

namespace MusicApp.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MusicAppDbContext _context;
        private MusicRepository _musicRepository;
        private ArtistRepository _artistRepository;

        public UnitOfWork(MusicAppDbContext context)
        {
            this._context = context;
        }

        public IMusicRepository Musics => _musicRepository = _musicRepository ?? new MusicRepository(_context);

        public IArtistRepository Artists => _artistRepository = _artistRepository ?? new ArtistRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}