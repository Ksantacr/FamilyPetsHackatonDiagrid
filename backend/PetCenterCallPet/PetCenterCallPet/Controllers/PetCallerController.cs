using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using PetCenterCallPet.Models;

namespace PetCenterCallPet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetCallerController : ControllerBase
    {
        private readonly DaprClient _client;
        public PetCallerController(DaprClient client)
        {
            _client = client;

        }
        [HttpPost]
        public async Task<IActionResult> ReportNewPet([FromForm] ReportPetRequestDto request)
        {
            if (ModelState.IsValid)
            {
                if (request.Photo != null && request.Photo.Length > 0)
                {
                    // Validate image format allowed
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var fileExtension = Path.GetExtension(request.Photo.FileName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("ImageFile", "Image format not allowed.");
                        return BadRequest(ModelState); // o vista con errores
                    }

                    // TODO: Save image

                    //var fileName = Guid.NewGuid() + fileExtension; // uniquename
                    //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                    // If we want save images locally 

                    //using (var stream = new FileStream(filePath, FileMode.Create))
                    //{
                    //    await request.Photo.CopyToAsync(stream);
                    //}

                    // TODO: Create the Event for target

                    await _client.InvokeMethodAsync(HttpMethod.Get, "target-pet", "api/petcenter");
                    // var result =  { new request should has: S3 url / other request information }
                    return Ok();
                }
                ModelState.AddModelError("ImageFile", "No file selected.");
            }

            return BadRequest(ModelState);

        }
    }
}
