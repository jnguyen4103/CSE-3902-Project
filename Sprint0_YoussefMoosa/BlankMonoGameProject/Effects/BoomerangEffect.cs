using Microsoft.Xna.Framework;

namespace Sprint02
{
    public class BoomerangEffect : IEffect
    {
        private Vector2 position;
        private readonly Vector2 velocity;
        private ISprite boomerangSprite;
        int vertDirection;

        public const int UpDirection = -1;
        public const int ForwardDirection = 0;
        public const int DownDirection = 1;

        public BoomerangEffect(Vector2 initialPos)
        {
            position = initialPos;
           /* boomerangSprite = new FireballSprite(initialPos); */
        }

        public void Update()
        {
        }
    }
}
