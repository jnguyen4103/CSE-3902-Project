using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0
{
    public interface ISprite
    {
        /// <summary>
        /// This method will update the current frame to allow SpriteBatch to
        /// draw a new frame.
        /// </summary>
        void Update();

        /// <summary>
        /// This method will draw the sprite according to inputs 
        /// to the controllers.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch object used by the game.</param>
        /// <param name="location">The location where the sprite will be drawn</param>
        void DrawSprite(SpriteBatch spriteBatch, Vector2 location);
    }
}
