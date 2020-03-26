namespace WuxiaWorld.DAL.Models {

    using System;

    public class ChapterInput : IdNameModel {
        public string Content { get; set; }
        public int Number { get; set; }
        public int NovelId { get; set; }
        public DateTime? ChapterPublishDate { get; set; }
    }

}