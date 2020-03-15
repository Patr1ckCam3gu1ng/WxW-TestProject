namespace WuxiaWorld.DAL.Entities {

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Chapters {

        [Key]
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

        [Required]
        [DataType(DataType.DateTime)]
        public int ChapterPublishDate { get; set; }

        public int NovelId { get; set; }

        [ForeignKey("NovelId")]
        public Novels Novels { get; set; }
    }

}