namespace WuxiaWorld.DAL.Models {

    public class ChapterModel {
        public int ChapterId { get; set; }
        public int NovelId { get; set; }
        public int ChapterNumber { get; set; }
        public string ChapterName { get; set; }
        public string Content { get; set; }
        public int TimeRead { get; set; }
    }

}