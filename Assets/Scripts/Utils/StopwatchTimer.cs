

using System.Diagnostics;

namespace PomoTimerApp
{
    public class StopwatchTimer
    {
        private float mScale = 1.0f;

        public StopwatchTimer(float scale = 1.0f)
        {
            mScale = scale;
        }

        public int Minutes => TotalMinutes % 25;

        public int Seconds => TotalSeconds % 60;

        private Stopwatch Stopwatch = new Stopwatch();

        public bool IsRunning => Stopwatch.IsRunning;

        public int TotalSeconds => (int)(Stopwatch.Elapsed.TotalSeconds * mScale);

        public int TotalMinutes => TotalSeconds / 60;

        public void Stop()
        {
            Stopwatch.Stop();
        }

        public void Start()
        {
            Stopwatch.Start();
        }
    }
}