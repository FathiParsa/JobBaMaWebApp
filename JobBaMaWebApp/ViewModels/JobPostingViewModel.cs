using System.ComponentModel.DataAnnotations;

namespace JobBaMaWebApp.ViewModels
{
    public class JobPostingViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
