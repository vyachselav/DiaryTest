using System;

namespace Diary.Web.Models.Note
{
    public class NotePreviewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
    }
}