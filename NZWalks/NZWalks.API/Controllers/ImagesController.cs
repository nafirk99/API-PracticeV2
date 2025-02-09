using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImgageRepository imgageRepository;

        public ImagesController(IImgageRepository imgageRepository)
        {
            this.imgageRepository = imgageRepository;
        }
        // POST api/images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO request)   // fromform is used to get the file from the form
        {
            ValidateImageUpload(request);

            if (ModelState.IsValid)
            {
                // Convert DTO to Domain Model
                var imageDomainMoldel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription
                };
                
                // User Repository to upload the image
                await imgageRepository.Upload(imageDomainMoldel);
                return Ok(imageDomainMoldel);
            }

            return BadRequest(ModelState);
        }

        private void ValidateImageUpload(ImageUploadRequestDTO request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported File Entension");
            }
            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size should not exceed 10MB");  
            }
        }
    }
}
