using TaskScheduler;

namespace TaskSchedulerWebAPI
{
    public class Task
    {
        public string Path { get; internal set; }
        public string Name { get; internal set; }
        public string State { get; internal set; }
        public DateTime LastRunTime { get; internal set; }
        public string LastTaskResult { get; internal set; }
        public DateTime NextRunTime { get; internal set; }
        public int NumberOfMissedRuns { get; internal set; }
        public string Enabled { get; internal set; }
        public int Id { get; internal set; }
    }
}
