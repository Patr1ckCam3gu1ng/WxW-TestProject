namespace WuxiaWorld.DAL.Models {

    using System;

    public class NovelChaptersResult {
        public int ChapterNumber { get; set; }
        public string Content { get; set; }
        public DateTime? ChapterPublishDate { get; set; }
    }

}