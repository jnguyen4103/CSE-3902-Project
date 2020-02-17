using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint02
{

    public class FireballSprite : ISprite
    {
        private Texture2D texture;
        private readonly SpriteBatch spriteBatch;
        private const int NumColumns = 4;
        private const int FireballWidth = 8;
        private const int FireballHeight = 10;

        private Rectangle toDraw = new Rectangle(0, 0, FireballWidth, FireballHeight);

        private const int FrameTime = 10;
        private int frameTimer = FrameTime;
        private int column = 0;

        Vector2 newPosition;
        private readonly Vector2 speed = new Vector2(0.5f, 0.5f);
        private Vector2 position;
        private Vector2 newPostion;
        private readonly Vector2 screemDim;

        public FireballSprite(Texture2D texture, SpriteBatch batch, Vector2 spawn)
        {
            this.texture = texture;
            this.spriteBatch = batch;
            position = newPosition = spawn;
        }

        public void DrawSprite()
        {
            /* Animate */
            frameTimer--;
            if (frameTimer <= 0)
            {
                column++;
                if (column >= NumColumns)
                {
                    column = 0;
                }
                toDraw.Y = column * FireballHeight;
                frameTimer = FrameTime;
            }

            /* Move */
            if (position.X > newPosition.X)
            {
                position.X -= speed.X;
            }
            else if (position.X < newPosition.X)
            {
                position.X += speed.X;
            }

            if (position.Y > newPosition.Y)
            {
                position.Y -= speed.Y;
            }
            else if (position.Y < newPosition.Y)
            {
                position.Y += speed.Y;
            }

            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, FireballWidth, FireballHeight), toDraw, Color.White);
        }

        public void MoveToPosition(Vector2 newPosition)
        {
            this.newPosition = this.position + newPosition;
            
        }

        public void UpdateSpriteFrames(int newAtlas) 
        { 
            /* Only one column of animation. */
        }

        public void UpdatePosition(Vector2 newPosition)
        {
            this.newPosition = newPosition;
        }

    }

}
