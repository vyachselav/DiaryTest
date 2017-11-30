using System.ComponentModel.DataAnnotations;

namespace Diary.Web.Models.Note
{
    public class NoteCreateModel
    {
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Title cannot be empty")]
        public string Title { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Text cannot be empty and should be shorter than 500 symbols")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        public PictureModel Picture { get; set; }
    }
}