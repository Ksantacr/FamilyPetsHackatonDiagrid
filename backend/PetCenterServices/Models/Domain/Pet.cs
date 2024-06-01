using System.ComponentModel.DataAnnotations;

namespace PetCenter.Models.Domain
{
    public class Pet
    {
        public Guid Id { get; set; }
        
        public required string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public string? Breed { get; set; }
        public List<byte>? Images { get; set; }
        public MedicalProfile? MedicalProfile { get; set; }
    }
}
