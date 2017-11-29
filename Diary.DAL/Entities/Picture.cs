using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diary.DAL.Entities
{
    public class Picture
    {
        [Key]
        [ForeignKey("Note")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string MIME { get; set; }
        public byte[] Image { get; set; }
        public Note Note { get; set; }

    }
}