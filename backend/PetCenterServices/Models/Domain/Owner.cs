using PetCenter.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PetCenter.Models.Domain
{
    public class Owner
    {

        public Guid Id { get; set; }
        public required string Names { get; set; }

        public required string LastNames { get; set; }

        public DateTime DateBirth { get; set; }

        public Gender Gender { get; set; }

        public required string PhoneNumber { get; set; }

        public List<Byte>? Images { get; set; }

        public required Address Address { get; set; }

        public List<Pet>? Pets { get; set; }
    }
}
