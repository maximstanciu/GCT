namespace GCT.Contracts.Data.Entities
{
    public class Account : BaseEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public Decimal Balance { get; set; }
    }
}
