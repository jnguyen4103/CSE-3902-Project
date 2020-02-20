using Microsoft.Xna.Framework;

/*
 *  Static class to consolidate what 
 *  the update time is in-game so that
 *  GameTime doesn't have to be passed
 *  around in every method requiring game
 *  time (which is a lot of methods).
 */

/*
 *  Sources:
 *
 *  R.B. Whitaker GameTime tutorial - http://rbwhitaker.wikidot.com/gametime
 */

namespace Sprint02
{
    public static class MonoTime
    {
        private static GameTime gameTime;

        public static float DeltaTime
        {
            get { return gameTime.ElapsedGameTime.Seconds; }
        }

        public static void Update(GameTime gameTime)
        {
            //this.gameTime = gameTime;
        }
    }
}

