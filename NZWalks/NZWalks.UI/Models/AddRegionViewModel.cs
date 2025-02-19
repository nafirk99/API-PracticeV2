using System.ComponentModel.DataAnnotations;

namespace NZWalks.UI.Models
{
    public class AddRegionViewModel
    {
        [Required(ErrorMessage = "Code is required.")]
        [MinLength(3, ErrorMessage = "Code Has To Be Minimum of 3 Letters")]
        [MaxLength(3, ErrorMessage = "Code Has To Be Maximum of 3 Letters")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name Has To Be Maximum Of 100 Letters")]
        public string Name { get; set; }
        public string RegionImgUrl { get; set; }
    }
}
