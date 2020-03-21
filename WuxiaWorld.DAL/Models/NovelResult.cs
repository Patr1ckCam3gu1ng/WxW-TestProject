namespace WuxiaWorld.DAL.Models {

    using System;
    using System.Collections.Generic;

    using Entities;

    public class NovelResult : IdNameModel {
        public DateTime TimeCreated { get; set; }
        public List<Chapters> Chapters { get; set; }
        public List<NovelGenreResult> Genres { get; set; }
    }

}