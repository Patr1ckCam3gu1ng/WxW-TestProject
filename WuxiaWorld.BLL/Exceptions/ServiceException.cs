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
        public FailedCreatingNewException(string message = "Failed creating new chapter for this chapter") :
            base(message) {
        }
    }

    public class RecordAlreadyExistsException : ServiceException {
        public RecordAlreadyExistsException(string message) : base(message) {
        }
    }

    public class NovelChapterNumberExistsException : ServiceException {
        public NovelChapterNumberExistsException(string message = "Chapter number already exists in this novel") :
            base(message) {
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
        public OneOrMoreGenreNotFoundException(string message = "Genre not found") : base(message) {
        }
    }

    public class NovelNotFoundException : ServiceException {
        public NovelNotFoundException(string message) : base(message) {
        }
    }

    public class ChapterAlreadyPublished : ServiceException {
        public ChapterAlreadyPublished(string message) : base(message) {
        }
    }

    public class OneOrMoreNovelAlreadyExists : ServiceException {
        public OneOrMoreNovelAlreadyExists(string message = "One or more novel already exists in the database") :
            base(message) {
        }
    }

    public class NovelGenreAlreadyExists : ServiceException {
        public NovelGenreAlreadyExists(string message = "Genre already assigned to this Novel") : base(message) {
        }
    }

}