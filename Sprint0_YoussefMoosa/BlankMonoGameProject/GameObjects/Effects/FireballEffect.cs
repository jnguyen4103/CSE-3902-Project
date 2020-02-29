using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint03
{
    public class FireballEffect
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

        public void CreateEffect()
        {
            //monoProcess.EffectsList.Add(new FireballSprite(effectTexture, spriteBatch, position,  -1, 0));
            //monoProcess.EffectsList.Add(new FireballSprite(effectTexture, spriteBatch, position, -1, -1));
            //monoProcess.EffectsList.Add(new FireballSprite(effectTexture, spriteBatch, position, -1, 1));

        }


    }
}
