using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint02
{
    class SwordBeamEffect : IEffect
    {
        private Texture2D effectTexture;
        private Game1 monoProcess;
        private readonly SpriteBatch spriteBatch;

        public SwordBeamEffect(Texture2D texture, SpriteBatch batch, Game1 monoInstance)
        {
            effectTexture = texture;
            spriteBatch = batch;
            monoProcess = monoInstance;
        }
        public void createEffectSprite(Vector2 position, int xDir, int yDir)
        {
            // Adds new sprite effect to the EffectsList array so it'll be drawn on screen
            monoProcess.EffectsList.Add(new SwordBeamSprite(effectTexture, spriteBatch, position, xDir, yDir));

        }

    }
}
