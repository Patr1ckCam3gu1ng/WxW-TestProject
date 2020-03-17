namespace WuxiaWorld.DAL.Entities {

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Newtonsoft.Json;

    public class NovelGenres {

        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        public int GenreId { get; set; }

        [ForeignKey("GenreId")]
        public Genres Genres { get; set; }

        [Required]
        public int NovelId { get; set; }

        [ForeignKey("NovelId")]
        public Novels Novels { get; set; }
    }

}