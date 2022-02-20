namespace GCT.Contracts.Data.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdCard { get; set; }
    }
}
