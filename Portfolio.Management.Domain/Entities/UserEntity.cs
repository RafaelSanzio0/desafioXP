using Portfolio.Management.Domain.Enums;

namespace Portfolio.Management.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserType Type { get; set; }

        public UserEntity() : base()
        {
        }
    }
}