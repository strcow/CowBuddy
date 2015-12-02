namespace CowLibrary.Utilities
{
    using System;
    using System.Collections.Concurrent;

    public static class Scheduler
    {
        private static readonly ConcurrentDictionary<Action, ScheduledTask> ScheduledTasks = new ConcurrentDictionary<Action, ScheduledTask>();

        public static void Execute(Action action, int timeoutMs)
        {
            var task = new ScheduledTask(action, timeoutMs);
            task.TaskComplete += RemoveTask;
            ScheduledTasks.TryAdd(action, task);
            task.Timer.Start();
        }

        private static void RemoveTask(object sender, EventArgs e)
        {
            var task = (ScheduledTask)sender;
            task.TaskComplete -= RemoveTask;
            ScheduledTask deleted;
            ScheduledTasks.TryRemove(task.Action, out deleted);
        }
    }
}
