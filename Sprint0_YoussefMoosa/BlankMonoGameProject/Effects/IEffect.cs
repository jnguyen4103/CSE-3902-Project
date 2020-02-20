
using Microsoft.Xna.Framework;

namespace Sprint02
{
    public interface IEffect
    {
        // Each effect will spawn a sprite on a set position and it'll move at
        // some velocity.
        // Look at FireballEffect for comments on implementation
        void createEffectSprite(Vector2 position , int xDir, int yDir);
    }
}