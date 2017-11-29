using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diary.DAL.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        public Picture Picture { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}