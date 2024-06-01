using PetCenter.Models.Enums;

namespace PetCenter.Models.Domain
{
    public class BulletinBoard
    {
        public required Guid Id { get; set; }
        public required DateTime CreationDate { get; set; }
        public DateTime? LastModification { get; set; }
        public required Status Status { get; set; }
        public required List<Bulletin> Bulletin { get; set; }

    }
}
