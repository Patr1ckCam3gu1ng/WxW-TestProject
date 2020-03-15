namespace WuxiaWorld.DAL.Entities {

    using System.ComponentModel.DataAnnotations;

    public class Genres {

        [Key]
        public int GenreId { get; set; }

        [Required]
        [StringLength(50)]
        public string GenreName { get; set; }
    }

}