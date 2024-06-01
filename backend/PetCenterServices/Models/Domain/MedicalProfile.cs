using PetCenter.Models.Enums;

namespace PetCenter.Models.Domain
{
    public class MedicalProfile
    {
        public Guid Id { get; set; }
        public string? VetName { get; set; }
        public List<string>? DiagnosedDisease { get; set; }
        public string? MedicalCareDetail { get; set; }
    }
}
