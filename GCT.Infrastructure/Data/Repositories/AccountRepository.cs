using GCT.Contracts.Data.Entities;
using GCT.Contracts.Data.Repositories;
using GCT.Core.Data.Repositories;
using GCT.Migrations;

namespace GCT.Infrastructure.Data.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
