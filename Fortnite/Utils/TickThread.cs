﻿using System;
using System.Threading;

namespace Fortnite.Utils
{
    public abstract class TickThread : IDisposable
    {
        protected TickThread(string threadName, int threadTickDelayMs = 1, int threadTimeoutMs = 3000)
        {
            ThreadName = threadName;
            ThreadTimeout = TimeSpan.FromMilliseconds(threadTimeoutMs);
            ThreadTickDelay = TimeSpan.FromMilliseconds(threadTickDelayMs);
            Thread = new Thread(ThreadStart)
            {
                Name = ThreadName
            };
        }

        private string ThreadName { get; }
        private TimeSpan ThreadTimeout { get; }
        private TimeSpan ThreadTickDelay { get; }
        private Thread Thread { get; set; }

        public virtual void Dispose()
        {
            Thread.Interrupt();
            if (!Thread.Join(ThreadTimeout)) Thread.Abort();

            Thread = default;
        }

        public void Start()
        {
            Thread.Start();
        }

        private void ThreadStart()
        {
            try
            {
                while (true)
                {
                    Tick();
                    Thread.Sleep(ThreadTickDelay);
                }
            }
            catch (ThreadInterruptedException)
            {
            }
        }

        protected abstract void Tick();
    }
}