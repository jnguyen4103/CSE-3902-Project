using Microsoft.Xna.Framework;

/*
 *  Timer interface to support timed events. 
 */

namespace Sprint02
{
    public interface ITimer 
    {
        /*
         *  Update the timer to make it
         *  ticks down to zero. 
         */
        void Update();
        /*
         *  This returns true when the
         *  timer ticks down to zero.
         */
        bool Alarm();
    }
}

