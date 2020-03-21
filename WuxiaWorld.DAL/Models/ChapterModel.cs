namespace WuxiaWorld.DAL.Models {

    public class ChapterModel : IdNameModel {
        public int ChapterNumber { get; set; }
        public string Content { get; set; }
        public int NovelId { get; set; }
    }

}