using System.ComponentModel.DataAnnotations;

namespace File_Convertor.Models
{
    public class UploadedFile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; }

        public string MovieName { get; set; }

        //[Required]
        public DateTime CreatedDate { get; set; }
    }
}
