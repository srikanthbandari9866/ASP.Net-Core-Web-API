using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IamgesController : ControllerBase
    {
        private readonly IImageRepo _imageRepo;
        public IamgesController(IImageRepo repo)
        {
            _imageRepo = repo;
        }

        // POST : /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDto imageUploadDto)
        {
            try
            {
                ValidateFileUpload(imageUploadDto);
                if (ModelState.IsValid)
                {
                    //convert dto to domainModel
                    var imageDomain = new Image
                    {
                        File = imageUploadDto.File,
                        FileName = imageUploadDto.FileName,
                        FileExtension = Path.GetExtension(imageUploadDto.File.FileName),
                        FileSizeInBytes = imageUploadDto.File.Length
                    };

                    await _imageRepo.ImageUpload(imageDomain);
                    return Ok(imageDomain);
                }
                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        private void ValidateFileUpload(ImageUploadDto imageUploadDto)
        {
            var allowedExtensions = new string[] {".jpg", ".jpeg", ".png", ".svg", ".pdf" }; 

            if(!allowedExtensions.Contains(Path.GetExtension(imageUploadDto.File.FileName))) 
            {
                ModelState.AddModelError("File", "File extension not supported");
            }
            if(imageUploadDto.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File size could not exceed the 10Mb");
            }
        }
    }
}
