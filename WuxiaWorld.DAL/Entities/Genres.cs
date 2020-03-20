namespace WuxiaWorld.DAL.Entities {

    using System.Collections.Generic;

    using Models;

    public class Genres : IdNameModel {

        public ICollection<NovelGenres> NovelGenres { get; set; }
    }

}