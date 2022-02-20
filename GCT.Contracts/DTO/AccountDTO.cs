namespace GCT.Contracts.DTO
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public Decimal Balance { get; set; }
        public DateTime AddedOn { get; set; }

    }
}
