using System;

namespace Diary.Web.Models.Api
{
    public class NoteApiModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        public PictureApiModel Picture { get; set; }
    }
}