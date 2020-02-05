using System;

namespace Azure.Serverless.Models {

    public class LogRecord {
        public string Uuid { get; } = Guid.NewGuid ().ToString ();
        public DateTime LogTimestamp { get; } = DateTime.UtcNow;
        public int LogLevel { get; set; }
        public string Text { get; set; }
    }

    public enum LogLevelEnum {
        ERROR = 0,
        WARNING = 1,
        INFO = 2,
        USEFUL_LOG = 3,

        // funny things
        UNUSEFUL_LOG = 4,
        WHAT_I_HAVE_IN_MIND = 5, // can be more useful than useful, use it !
    }
}