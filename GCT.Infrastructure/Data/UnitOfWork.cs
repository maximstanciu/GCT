using GCT.Contracts.Data;
using GCT.Contracts.Data.Repositories;
using GCT.Infrastructure.Data.Repositories;
using GCT.Migrations;

namespace GCT.Infrastructure.Data
{
    /// <summary>
    /// Unit of Work Pattern Trigger Implementation
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }
        public IUserRepository Users => new UserRepository(_context);
        public IAccountRepository Accounts => new AccountRepository(_context);
        public ITransactionRepository Transactions => new TransactionRepository(_context);


        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}