using Microsoft.EntityFrameworkCore;
using GCT.Contracts.Data.Entities;

namespace GCT.Migrations
{
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// To Init Migration Run: dotnet ef migrations add InitialCreate --project GCT.Migrations --startup-project GCT.API
        /// </summary>
        /// <param name="options"></param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries<BaseEntity>().AsEnumerable())
            {
                //Auto Timestamp
                item.Entity.AddedOn = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}