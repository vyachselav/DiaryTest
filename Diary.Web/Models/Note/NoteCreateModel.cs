using System.ComponentModel.DataAnnotations;

namespace Diary.Web.Models.Note
{
    public class NoteCreateModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        public PictureModel Picture { get; set; }
    }
}