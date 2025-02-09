using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class ImageUploadRequestDTO
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }  // ? means it can be null, Not a required field
    }
}
