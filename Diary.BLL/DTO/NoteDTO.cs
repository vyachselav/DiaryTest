using System;

namespace Diary.BLL.DTO
{
    public class NoteDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool Deletable { get; set; }
        public DateTime CreationTime { get; set; }
        public PictureDTO Picture { get; set; }
        public bool WithPicture { get; set; }
        public string UserId { get; set; }
    }
}
