namespace CowLibrary.Utilities
{
    using System;
    using System.Timers;

    internal class ScheduledTask
    {
        public ScheduledTask(Action action, int timeoutMs)
        {
            this.Action = action;
            this.Timer = new Timer { Interval = timeoutMs };
            this.Timer.Elapsed += this.TimerElapsed;
        }

        public Action Action { get; private set; }

        public Timer Timer { get; private set; }

        public EventHandler TaskComplete { get; set; }

        private void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Timer.Stop();
            this.Timer.Elapsed -= this.TimerElapsed;
            this.Timer = null;

            this.Action();
            this.TaskComplete(this, EventArgs.Empty);
        }

    }
}
