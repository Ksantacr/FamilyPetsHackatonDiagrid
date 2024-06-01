namespace PetCenter.Models.Domain
{
    public class Address
    {
        public required string FullAddress { get; set; }
        public double? AddressGeo { get; set; }
    }
}
