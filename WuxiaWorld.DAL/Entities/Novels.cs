namespace WuxiaWorld.DAL.Entities {

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Models;

    public class Novels : IdNameModel {

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TimeCreated { get; set; }

        public ICollection<Chapters> Chapters { get; set; }
        public ICollection<NovelGenres> NovelGenres { get; set; }
    }

}