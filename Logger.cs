using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS4FreeConsole
{
    public enum LogType
    {
        Info,
        Warn,
        Error,
        Debug
    }

    public class LogMessage : EventArgs
    {
        #region Fields
        private String message;
        private LogType logType;
        private DateTime timestamp;
        #endregion

        #region Constructors
        public LogMessage(string msg) : this(msg, LogType.Info)
        {
        }
        public LogMessage(string msg, LogType type)
        {
            message = msg;
            logType = type;
            timestamp = DateTime.UtcNow;
        }
        #endregion

        #region Properties
        public String Message
        {
            get
            {
                return message;
            }
        }
        public LogType LogType
        {
            get
            {
                return logType;
            }
        }
        public DateTime Timestamp
        {
            get
            {
                return timestamp;
            }
        }
        public String FormatedMessage
        {
            get
            {
                return Timestamp.ToString("HH:mm:ss") + " " + Message;
            }
        }
        #endregion
    }

    public static class Logger
    {
        public static EventHandler<LogMessage> OnMessage;

        public static void LogMessage(string msg)
        {
            LogMessage(msg, LogType.Info);
        }
        public static void LogMessage(string msg, LogType type)
        {
            EventHandler<LogMessage> msgEvent = OnMessage;
            if (msgEvent != null)
            {
                msgEvent(null, new LogMessage(msg, type));
            }
        }
    }
}
