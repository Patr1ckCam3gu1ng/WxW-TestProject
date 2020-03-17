namespace WuxiaWorld.BLL.Exceptions {

    public class ServiceException : WuxiaWorldException {
        protected ServiceException(string message) : base(message) {
        }
    }

    public class NoRecordFoundException : ServiceException {
        public NoRecordFoundException(string message) : base(message) {
        }
    }

    public class FailedCreatingNewException : ServiceException {
        public FailedCreatingNewException(string message) : base(message) {
        }
    }

    public class RecordAlreadyExistsException : ServiceException {
        public RecordAlreadyExistsException(string message) : base(message) {
        }
    }

    public class NovelChapterNumberExistsException : ServiceException {
        public NovelChapterNumberExistsException(string message) : base(message) {
        }
    }

    public class FailedToPublishChapterException : ServiceException {
        public FailedToPublishChapterException(string message) : base(message) {
        }
    }

    public class NovelChapterNotFoundException : ServiceException {
        public NovelChapterNotFoundException(string message) : base(message) {
        }
    }

    public class OneOrMoreGenreNotFoundException : ServiceException {
        public OneOrMoreGenreNotFoundException(string message) : base(message) {
        }
    }

}