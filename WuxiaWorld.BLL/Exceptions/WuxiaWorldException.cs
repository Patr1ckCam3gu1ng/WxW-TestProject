namespace WuxiaWorld.BLL.Exceptions {

    using System;

    public class WuxiaWorldException : Exception {
        protected WuxiaWorldException(string message) : base(message) {
        }
    }

}