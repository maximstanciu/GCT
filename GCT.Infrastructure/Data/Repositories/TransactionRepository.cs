using GCT.Contracts.Data.Entities;
using GCT.Contracts.Data.Repositories;
using GCT.Core.Data.Repositories;
using GCT.Migrations;

namespace GCT.Infrastructure.Data.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
