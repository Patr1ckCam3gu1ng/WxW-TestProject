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

}