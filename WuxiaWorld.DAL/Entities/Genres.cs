namespace WuxiaWorld.DAL.Entities {

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Newtonsoft.Json;

    public class Genres {

        [Key]
        [JsonProperty("Id")]
        public int GenreId { get; set; }

        [Required]
        [StringLength(50)]
        [JsonProperty("Name")]
        public string GenreName { get; set; }
    }

}