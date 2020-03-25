namespace WuxiaWorld.DAL.Models {

    public class ChapterModel : IdNameModel {
        public int Number { get; set; }
        public string Content { get; set; }
        public int NovelId { get; set; }
    }

}