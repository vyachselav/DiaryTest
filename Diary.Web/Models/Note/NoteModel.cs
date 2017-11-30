using System;

namespace Diary.Web.Models.Note
{
    public class NoteModel
    {
        public int Id { get; set; }
        public bool Deletable { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        public bool WithPicture { get; set; }
        public string PathToPicture { get; set; }
    }
}