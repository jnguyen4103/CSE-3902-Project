using Microsoft.Xna.Framework;

namespace Sprint02
{
    public class MonoTimer : ITimer
    {
        private float secondsLeft;

        public MonoTimer(float secs)
        {
            secondsLeft = secs;
        }

        public void Update()
        {
            secondsLeft -= MonoTime.DeltaTime;
        }

        public bool Alarm()
        {
            return secondsLeft <= 0.0f;
        }
    }
}

