namespace WuxiaWorld.DAL.Models {

    using System;
    using System.Collections.Generic;

    public class NovelResult : IdNameModel {
        public DateTime TimeCreated { get; set; }
        public List<NovelChaptersResult> Chapters { get; set; }
        public List<NovelGenreResult> Genres { get; set; }
    }

}