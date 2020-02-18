using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint02
{
    public class BoomerangEffect : IEffect
    {
        // Fireball effect requires a sprite, a game process to access the EffectsList array
        // and a batch so the sprite can draw
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
            // Adds new sprite effect to the EffectsList array so it'll be drawn on screen
            monoProcess.EffectsList.Add(new BoomerangSprite(effectTexture, spriteBatch, position, xDir, yDir));

        }
    }
}
