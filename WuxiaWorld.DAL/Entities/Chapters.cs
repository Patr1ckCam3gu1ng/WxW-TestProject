namespace WuxiaWorld.DAL.Entities {

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Newtonsoft.Json;

    public class Chapters {

        [Key]
        [JsonIgnore]
        public int ChapterId { get; set; }

        [Required]
        public int ChapterNumber { get; set; }

        [Required]
        [StringLength(250)]
        public string ChapterName { get; set; }

        [Required]
        [StringLength(int.MaxValue)]
        public string Content { get; set; }

        [Required]
        public int TimeRead { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ChapterPublishDate { get; set; }

        [JsonIgnore]
        public int NovelId { get; set; }

        [JsonIgnore]
        [ForeignKey("NovelId")]
        public Novels Novels { get; set; }
    }

}