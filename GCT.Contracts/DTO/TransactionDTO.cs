namespace GCT.Contracts.DTO
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public string AccountId { get; set; }
        public string UserId { get; set; }
        public Decimal Amount { get; set; }
        public DateTime AddedOn { get; set; }
        public string Type { get; set; }
    }
}
