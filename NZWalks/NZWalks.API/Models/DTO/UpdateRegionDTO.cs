using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code Has To Be Minimum Of 3 Letters")]
        [MaxLength(3, ErrorMessage = "Code has To Be Maximum Of 3 Letters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name has To Be Maximum Of 100 Letters")]
        public string Name { get; set; }
        public string? RegionImgUrl { get; set; }
    }
}
