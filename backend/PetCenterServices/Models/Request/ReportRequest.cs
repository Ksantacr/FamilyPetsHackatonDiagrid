namespace PetCenter.Models.Request
{
    public class ReportRequest
    {
        public Guid PetId { get; set; }
        public Guid Owner { get; set; }
        public DateTime DateLostPet { get; set; }
        public double? lastGeoLocalization { get; set; }
        public string? lastAddress { get; set; }
        public bool MedicalRisk { get; set; }
    }
}
