namespace GCT.Contracts.Data.Entities
{
    public class Transaction : BaseEntity
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public Decimal Amount { get; set; }
        public string Type { get; set; }
    }
}