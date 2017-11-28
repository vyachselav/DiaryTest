using System.Collections.Generic;

namespace Diary.Web.Models.Note
{
    public class PageModel
    {
        public IEnumerable<NotePreviewModel> Notes { get; set; }
        public PageInfo PageInfo { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}