using GCT.Contracts.Data.Repositories;

namespace GCT.Contracts.Data
{
    /// <summary>
    /// Unit of Work Pattern Trigger Interface
    /// </summary>
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IAccountRepository Accounts { get; }
        ITransactionRepository Transactions { get; }
        Task CommitAsync();
    }
}