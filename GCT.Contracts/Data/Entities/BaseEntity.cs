using System.ComponentModel.DataAnnotations;

namespace GCT.Contracts.Data.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual DateTime AddedOn { get; set; }
    }
}
