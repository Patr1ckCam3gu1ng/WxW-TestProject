namespace WuxiaWorld.DAL.Models {

    using System.Collections.Generic;

    public class NovelModel {
        public string Name { get; set; }
        public string Synopsis { get; set; }
        public List<int> GenreIds { get; set; }
    }

}