namespace WuxiaWorld.DAL.Models {

    using System.Collections.Generic;

    public class CommandModel {
        public string Text { get; set; }
        public bool IsAdmin { get; set; }
        public List<CommandModel> Children { get; set; }
    }

}