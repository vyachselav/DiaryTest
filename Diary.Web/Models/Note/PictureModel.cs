namespace Diary.Web.Models.Note
{
    public class PictureModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MIME { get; set; }
        public byte[] Image { get; set; }
    }
}