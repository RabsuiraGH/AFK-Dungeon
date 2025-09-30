using System;
using System.Collections.Generic;

namespace LA.BattleLog
{
    public class BattleLogService
    {
        private static BattleLogService _instance;

        public IReadOnlyCollection<LogEntry> Logs => _logs;

        public event Action<LogEntry> OnAddLog;
        public event Action OnClearLog;

        private List<LogEntry> _logs = new();


        public BattleLogService()
        {
            _instance = this;
        }


        public static void LogStatic(string message)
        {
            _instance.Log(message);
        }


        public static void LogStatic(string message, string tag)
        {
            _instance.Log(message, tag);
        }

        public static void LogSeparatorStatic()
        {
            _instance.Log("", "SEPARATOR");
        }

        public void Log(string message)
        {
            Log(message, string.Empty);
        }


        public void Log(string message, string tag)
        {
            LogEntry entry = LogEntry.Create(message, tag);
            _logs.Add(entry);
            OnAddLog?.Invoke(entry);
        }

        public void LogSeparator()
        {
            Log("", "SEPARATOR");
        }
        public void ClearLog()
        {
            _logs.Clear();
            OnClearLog?.Invoke();
        }
    }
}