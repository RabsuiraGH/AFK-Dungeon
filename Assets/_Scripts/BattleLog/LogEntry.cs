namespace LA.BattleLog
{
    public struct LogEntry
    {
        public string Tag { get; private set; }
        public string Message { get; private set; }

        public static LogEntry Create(string message, string tag) => new() { Tag = tag, Message = message };
    }
}