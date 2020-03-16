namespace WuxiaWorld.DAL.Entities {

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Novels {

        [Key]
        public int NovelId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(1500)]
        public string Synopsis { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TimeCreated { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? TimePublished { get; set; }

        public ICollection<Chapters> Chapters { get; set; }
        public ICollection<NovelGenres> NovelGenres { get; set; }
    }

}