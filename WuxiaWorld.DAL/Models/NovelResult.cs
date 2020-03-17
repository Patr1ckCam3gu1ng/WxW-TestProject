namespace WuxiaWorld.DAL.Models {

    using System;
    using System.Collections.Generic;

    using Entities;

    public class NovelResult {
        public int NovelId { get; set; }
        public string Name { get; set; }
        public string Synopsis { get; set; }
        public DateTime TimeCreated { get; set; }
        public List<Chapters> Chapters { get; set; }
        public List<NovelGenreResult> Genres { get; set; }
    }

}