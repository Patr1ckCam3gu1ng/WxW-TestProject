namespace WuxiaWorld.DAL.Models {

    using System;

    public class ChapterModel : IdNameModel {
        public int Number { get; set; }
        public int NovelId { get; set; }
        public DateTime? ChapterPublishDate { get; set; }
    }

}