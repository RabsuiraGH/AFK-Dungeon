using System;

namespace LA.BattleLog
{
    public struct LogEntry
    {
        public string Tag { get; set; }
        public string Message { get; set; }

        public static LogEntry Create(string message, string tag) => new LogEntry { Tag = tag, Message = message };
    }
}