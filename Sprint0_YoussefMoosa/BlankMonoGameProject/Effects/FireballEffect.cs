using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint02
{
    public class FireballEffect : IEffect
    {
        private Texture2D effectTexture;
        private Game1 monoProcess;
        private readonly SpriteBatch spriteBatch;

        public FireballEffect(Texture2D texture, SpriteBatch batch, Game1 monoInstance)
        {
            effectTexture = texture;
            spriteBatch = batch;
            monoProcess = monoInstance;
        }

        public void createEffectSprite(Vector2 position)
        {
            monoProcess.EffectsList.Add(new FireballSprite(effectTexture, spriteBatch, position,  -1, 0));
            monoProcess.EffectsList.Add(new FireballSprite(effectTexture, spriteBatch, position, -1, -1));
            monoProcess.EffectsList.Add(new FireballSprite(effectTexture, spriteBatch, position, -1, 1));

        }
    }
}
