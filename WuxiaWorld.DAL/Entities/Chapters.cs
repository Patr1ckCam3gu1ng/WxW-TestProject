namespace WuxiaWorld.DAL.Entities {

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Models;

    public class Chapters : IdNameModel {

        [Required]
        public int ChapterNumber { get; set; }

        [Required]
        [StringLength(int.MaxValue)]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ChapterPublishDate { get; set; }

        public int NovelId { get; set; }

        [ForeignKey(nameof(NovelId))]
        public Novels Novels { get; set; }
    }

}