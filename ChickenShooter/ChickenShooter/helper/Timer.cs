using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ChickenShooter.helper
{
    class Timer
    {
        private Stopwatch stopWatch;

        public long ElapsedMilliSeconds
        {
            get { return stopWatch.ElapsedMilliseconds; }
        }

        public long ElapsedTicks
        {
            get { return stopWatch.ElapsedTicks; }
        }

        public Timer()
        {
            stopWatch = new Stopwatch();
            stopWatch.Reset();
        }



        public void Start()
        {
            if (!stopWatch.IsRunning)
            {
                stopWatch.Reset();
                stopWatch.Restart();
            }
        }

        public void Stop()
        {
            stopWatch.Stop();
        }



    }
}
