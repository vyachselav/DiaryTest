using System;

namespace Diary.Web.Models.Note
{
    public class NoteModel : NoteCreateModel
    {
        public int Id { get; set; }
        public bool Deletable { get; set; }
        public DateTime CreationTime { get; set; }
        public string PathToPicture { get; set; }
    }
}