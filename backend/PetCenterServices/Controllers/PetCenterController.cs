using Microsoft.AspNetCore.Mvc;
using PetCenter.Interfaces;
using PetCenter.Models.Domain;
using PetCenter.Models.Request;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetCenterController : ControllerBase
    {

        private readonly IPetCenterServices petCenterServices;
        private readonly IPetCenterRepository petCenterRepository;
        private readonly ILogger<PetCenterController> logger;
        public PetCenterController(IPetCenterServices petCenterServices, IPetCenterRepository petCenterRepository, ILogger<PetCenterController> logger)
        {
            this.petCenterServices = petCenterServices;
            this.petCenterRepository = petCenterRepository;
            this.logger = logger;
        }

        // GET: api/<PetCenterController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost("inscriptions/pet")]
        public string RegisterPet([FromBody] Pet pet)
        {
            return "value";
        }

        [HttpPost("inscriptions/owner")]
        public string RegisterOwner([FromBody] Owner owner)
        {
            return "value";
        }

        [HttpPost("alert/report/lostpet")]
        public string RegisterReportPetLost([FromBody] ReportRequest report)
        {
            return "value";
        }

        // POST api/<PetCenterController>
        [HttpGet("bulletinBoard")]
        public string GetBulletinBoard()
        {
            return "value";
        }

        // PUT api/<PetCenterController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PetCenterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
