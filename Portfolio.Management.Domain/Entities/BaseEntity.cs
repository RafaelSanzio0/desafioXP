using Portfolio.Management.Domain.Abstractions;

namespace Portfolio.Management.Domain.Entities
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }

        public BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
            CreatedBy = CreatorHelper.GetEntityCreatorIdentity();
        }
    }
}