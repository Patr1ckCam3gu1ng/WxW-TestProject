namespace WuxiaWorld.DAL.Models {

    using System.Collections.Generic;

    public class NovelModel : IdNameModel {
        public List<int> GenreIds { get; set; }
    }

}