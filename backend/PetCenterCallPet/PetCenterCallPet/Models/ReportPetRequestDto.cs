using System.ComponentModel.DataAnnotations;

namespace PetCenterCallPet.Models
{
    public class ReportPetRequestDto
    {
        public string Name { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        public Location? Location { get; set; }
    }

    public class Location
    {
        public float? Lat { get; set; }
        public float? Lng { get; set; }
    }
}
