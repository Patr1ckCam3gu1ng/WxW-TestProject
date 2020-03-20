namespace WuxiaWorld.DAL.Models {

    using System.ComponentModel.DataAnnotations;

    public class IdNameModel {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }
    }

}