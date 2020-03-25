namespace WuxiaWorld.DAL.Models {

    using System.Collections.Generic;

    public class ChapterNovelResult {
        public List<ChapterContentModel> WithContents { get; set; }
        public List<ChapterModel> WithoutContents { get; set; }
    }

}