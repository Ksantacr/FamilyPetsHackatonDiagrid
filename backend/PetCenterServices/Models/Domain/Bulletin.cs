using PetCenter.Models.Enums;

namespace PetCenter.Models.Domain
{
    public class Bulletin
    {
        public required Guid Id { get; set; }

        public required DateTime CreationTime { get; set; }

        public required BulletinType BulletinType { get; set; } = 0;

        public required Status Status { get; set; } = 0;

        public required Report Report { get; set; }
    }
}
