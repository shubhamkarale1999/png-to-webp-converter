using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace File_Convertor.Models
{
    public class FileUploadViewModel
    {
        [Required]
        [Display(Name = "Select Files")]
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    }
}
