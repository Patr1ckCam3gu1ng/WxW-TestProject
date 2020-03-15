namespace WuxiaWorld.BLL.Exceptions {

    public class GenreServiceException : WuxiaWorldException {
        protected GenreServiceException(string message) : base(message) {
        }
    }

    public class NoRecordFoundException : GenreServiceException {
        public NoRecordFoundException(string message) : base(message) {
        }
    }

    public class FailedCreatingNewException : GenreServiceException {
        public FailedCreatingNewException(string message) : base(message) {
        }
    }
    public class GenreAlreadyExistsException : GenreServiceException
    {
        public GenreAlreadyExistsException(string message) : base(message)
        {
        }
    }

}