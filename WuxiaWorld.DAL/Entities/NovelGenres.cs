namespace WuxiaWorld.DAL.Entities {

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class NovelGenres {
        [Key]
        public string Id { get; set; }

        public int GenreId { get; set; }

        [ForeignKey("GenreId")]
        public Genres Genres { get; set; }

        public int NovelId { get; set; }

        [ForeignKey("NovelId")]
        public Novels Novels { get; set; }
    }

}