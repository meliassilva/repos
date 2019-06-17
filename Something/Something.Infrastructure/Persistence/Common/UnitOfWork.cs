using System.Threading;
using System.Threading.Tasks;
using Byui.Something.ApplicationCore.Common.Interfaces.Persistence;

namespace Byui.Something.Infrastructure.Persistence.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.SaveEntitiesAsync(cancellationToken);
        }
    }
}