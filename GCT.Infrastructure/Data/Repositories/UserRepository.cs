using GCT.Contracts.Data.Entities;
using GCT.Contracts.Data.Repositories;
using GCT.Core.Data.Repositories;
using GCT.Migrations;

namespace GCT.Infrastructure.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
