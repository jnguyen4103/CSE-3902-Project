using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint02
{
    public class FireballEffect : IEffect
    {
        private Vector2 position;
        private readonly Vector2 velocity;
        private ISprite fireballSprite;
        int vertDirection;

        public const int UpDirection = -1;
        public const int ForwardDirection = 0;
        public const int DownDirection = 1;

        public FireballEffect(SpriteBatch batch, Texture2D texture, Vector2 initialPos) 
        {
            position = initialPos;
            fireballSprite = new FireballSprite(texture, batch, initialPos);
        }

        public void Update()
        {
        }
    }
}
