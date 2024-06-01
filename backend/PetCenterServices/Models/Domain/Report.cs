using PetCenter.Models.Enums;

namespace PetCenter.Models.Domain
{
    public class Report
    {
        public required Guid PetId { get; set; }
        public required Owner Owner { get; set; }
        public double? LastPositionGeo { get; set; }
        public required string LastPositionSector { get; set; }
        public PetCondition PetCondition { get; set; }
    }
}
