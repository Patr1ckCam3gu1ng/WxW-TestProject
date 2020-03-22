namespace WuxiaWorld.DAL.Entities {

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class NovelGenres {

        [Key]
        public int Id { get; set; }

        [Required]
        public int GenreId { get; set; }

        [ForeignKey(nameof(GenreId))]
        public Genres Genres { get; set; }

        [Required]
        public int NovelId { get; set; }

        [ForeignKey(nameof(NovelId))]
        public Novels Novels { get; set; }
    }

}