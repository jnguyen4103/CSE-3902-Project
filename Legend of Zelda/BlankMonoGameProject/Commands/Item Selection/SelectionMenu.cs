using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint03
{
    public class SelectionMenu : ISelectionMenu
    {
        public const int TSquareX = 0;
        public const int TSquareY = 48;
        private int sSquareX = 512;
        private int sSquareY = 188;
        private Texture2D sqTex;
        private Texture2D tileSprite;
        private Texture2D itemSprite;
        private SpriteBatch batch;
        private Game1 debug;
    

        public SelectionMenu(Game1 game)
        {
            sqTex = game.ItemSpriteSheet;
            tileSprite = game.TileSpriteSheet;
            itemSprite = game.ItemSpriteSheet;
            batch = game.spriteBatch;
            debug = game;
        }

        public void SelectionUp() 
        {
            sSquareY -= 4;
        }

        public void SelectionDown() 
        {
            sSquareY += 4;
        }

        public void SelectionLeft() 
        {
            sSquareX -= 4;
        }

        public void SelectionRight() 
        {
            sSquareX += 4;
        }

        public void Choose() 
        { 
        
        }

        public void Draw()
        {
            foreach (IItem element in menuList)
            {
               

            }
            batch.Draw(tileSprite, new Rectangle(500, 200, 350, 120),
                new Rectangle(96, 0, 8, 8), Color.White);
            batch.Draw(sqTex, new Rectangle(sSquareX, sSquareY, 48, 48),
                new Rectangle(TSquareX, TSquareY, 16, 16), Color.White);
           /* batch.Draw(itemSprite, new Rectangle(520, 200, 25, 25),
               new Rectangle(140, 0, 8, 8), Color.White);
            batch.Draw(itemSprite, new Rectangle(580, 200, 25, 25),
               new Rectangle(112, 0, 8, 8), Color.White);*/
          
        }
    }
}
