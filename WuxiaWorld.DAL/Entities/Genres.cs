namespace WuxiaWorld.DAL.Entities {

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    public class Genres {

        [Key]
        [JsonProperty("Id")]
        public int GenreId { get; set; }

        [Required]
        [StringLength(50)]
        [JsonProperty("Name")]
        public string GenreName { get; set; }

        public ICollection<NovelGenres> NovelGenres { get; set; }
    }

}