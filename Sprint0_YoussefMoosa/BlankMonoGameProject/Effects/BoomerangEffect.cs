using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint02
{
    public class BoomerangEffect : IEffect
    {
        private Texture2D effectTexture;
        private Game1 monoProcess;
        private readonly SpriteBatch spriteBatch;

        public BoomerangEffect(Texture2D texture, SpriteBatch batch, Game1 monoInstance)
        {
            effectTexture = texture;
            spriteBatch = batch;
            monoProcess = monoInstance;
        }

        public void createEffectSprite(Vector2 position, int xDir, int yDir)
        {

            monoProcess.EffectsList.Add(new BoomerangSprite(effectTexture, spriteBatch, position, xDir, yDir));

        }
    }
}
