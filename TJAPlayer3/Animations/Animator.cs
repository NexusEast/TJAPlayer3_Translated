using System;
using FDK;

namespace TJAPlayer3.Animations
{
    internal abstract class Animator : IAnimatable
    {
        protected Animator(int startValue, int endValue, int tickInterval, bool isLoop)
        {
            Type = CounterType.Normal;
            StartValue = startValue;
            EndValue = endValue;
            TickInterval = tickInterval;
            IsLoop = isLoop;
            Counter = new CCounter();
        }
        protected Animator(double startValue, double endValue, double tickInterval, bool isLoop)
        {
            Type = CounterType.Double;
            StartValue = startValue;
            EndValue = endValue;
            TickInterval = tickInterval;
            IsLoop = isLoop;
            Counter = new CCounter();
        }
        public void Start()
        {
            if (Counter == null) throw new NullReferenceException();
            switch (Type)
            {
                case CounterType.Normal:
                    Counter.tStart((int)StartValue, (int)EndValue, (int)TickInterval, TJAPlayer3.Timer);
                    break;
                case CounterType.Double:
                    Counter.tStart((double)StartValue, (double)EndValue, (double)TickInterval, CSoundManager.rPlaybackTimer);
                    break;
                default:
                    break;
            }
        }
        public void Stop()
        {
            if (Counter == null) throw new NullReferenceException();
            Counter.tStop();
        }
        public void Reset()
        {
            if (Counter == null) throw new NullReferenceException();
            Start();
        }

        public void Tick()
        {
            if (Counter == null) throw new NullReferenceException();
            switch (Type)
            {
                case CounterType.Normal:
                    if (IsLoop) Counter.tStartLoop(); else Counter.tStart();
                    if (!IsLoop && Counter.bEnded) Stop();
                    break;
                case CounterType.Double:
                    if (IsLoop) Counter.tStartLoopDb(); else Counter.tStartdb();
                    if (!IsLoop && Counter.bEndeddb) Stop();
                    break;
                default:
                    break;
            }
        }

        public virtual object GetAnimation()
        {
            throw new NotImplementedException();
        }



        // プロパティ
        public CCounter Counter
        {
            get;
            private set;
        }
        public CounterType Type
        {
            get;
            private set;
        }
        public object StartValue
        {
            get;
            private set;
        }
        public object EndValue
        {
            get;
            private set;
        }
        public object TickInterval
        {
            get;
            private set;
        }
        public bool IsLoop
        {
            get;
            private set;
        }
    }

    enum CounterType
    {
        Normal,
        Double
    }
}
