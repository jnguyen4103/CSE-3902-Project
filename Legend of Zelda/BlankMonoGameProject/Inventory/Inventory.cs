/* Contributors
* Stephen Hogg
* Grant Gabel
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Sprint03
{
    public class Inventory
    {
        private Game1 Game;
        private readonly Texture2D inventoryScreen;
        private readonly Texture2D inventoryExtras;
        public Vector2 Size;

        private float offset = 2;
        private Vector2 initialCameraPosition;

        public StaticSprite MainInventory;
        public StaticSprite MiniMap;
        public StaticSprite Map;
        public StaticSprite WeaponA;
        public StaticSprite WeaponB;
        public StaticSprite[] LifeBar;
        public StaticSprite[] CurrentLife;
        public StaticSprite[] Rupees;
        public StaticSprite[] Keys;
        public StaticSprite[] Bombs;
        public StaticSprite LevelNumber;

        public Inventory(Game1 game)
        {
            Game = game;
            Size = new Vector2(256, 240);
            inventoryScreen = Game.Content.Load<Texture2D>("HUD");
            inventoryExtras = Game.Content.Load<Texture2D>("HUD Extras");

            SetupHUDSprites();
            
        }
        public void Draw()
        {
            initialCameraPosition = Game.Camera.Position;
            Vector2 location = new Vector2(initialCameraPosition.X / Game.ScreenScale, (initialCameraPosition.Y - offset * Game.ScreenScale) / Game.ScreenScale);
            MainInventory.UpdatePosition(location);
            MainInventory.DrawSprite();
            MiniMap.UpdatePosition(new Vector2(initialCameraPosition.X / Game.ScreenScale + 16, ((initialCameraPosition.Y - Game.ScreenScale * offset) / Game.ScreenScale) + 192));
            MiniMap.DrawSprite();
            WeaponA.UpdatePosition(new Vector2(location.X + 152f, location.Y + 208f));
            WeaponA.DrawSprite();

            // Item Draw and Update loop
            for (int i = 0; i < Rupees.Length; i++)
            {
                Rupees[i].UpdatePosition(new Vector2(location.X + 104 + 8 * i, location.Y + 200));
                Keys[i].UpdatePosition(new Vector2(location.X + 104 + 8 * i, location.Y + 216));
                Bombs[i].UpdatePosition(new Vector2(location.X + 104 + 8 * i, location.Y + 224));
                Rupees[i].DrawSprite();
                Keys[i].DrawSprite();
                Bombs[i].DrawSprite();
            }

            // Health Draw and Update loop
            for (int i = 0; i < LifeBar.Length; i++)
            {
                if (i < 8)
                {
                    LifeBar[i].UpdatePosition(new Vector2(location.X + 176 + 8 * i, location.Y + 216));
                    CurrentLife[i].UpdatePosition(new Vector2(location.X + 176 + 8 * i, location.Y + 216));

                }
                else
                {
                    LifeBar[i].UpdatePosition(new Vector2(location.X + 176 + 8 * (i - 8), location.Y + 224));
                    CurrentLife[i].UpdatePosition(new Vector2(location.X + 176 + 8 * (i - 8), location.Y + 224));



                }
                LifeBar[i].DrawSprite();
                if (!LifeBar[i].Colour.Equals(Color.Black)) {
                    CurrentLife[i].DrawSprite();
                }
            }
        }

        private void SetupHUDSprites()
        {
            MainInventory = new StaticSprite(Game, "HUD", Vector2.Zero, inventoryScreen, Game.spriteBatch);
            MainInventory.Layer = 0.97f;

            MiniMap = new StaticSprite(Game, "Dungeon01Map", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MiniMap.Layer = 1f;
            MiniMap.Colour = Color.Black;

            WeaponA = new StaticSprite(Game, "RedLightsaber", Vector2.Zero, Game.EffectSpriteSheet, Game.spriteBatch);
            WeaponA.Layer = 1f;
            WeaponA.TotalFrames = 1;

            LifeBar = new StaticSprite[16];
            for(int i = 0; i < LifeBar.Length; i++)
            {
                LifeBar[i] = new StaticSprite(Game, "EmptyHeart", Vector2.Zero, inventoryExtras, Game.spriteBatch);
                LifeBar[i].Layer = 0.98f;
                if(i > 2)
                {
                    LifeBar[i].Colour = Color.Black;
                }
            }

            CurrentLife = new StaticSprite[16];
            for (int i = 0; i < LifeBar.Length; i++)
            {
                CurrentLife[i] = new StaticSprite(Game, "FullHeart", Vector2.Zero, inventoryExtras, Game.spriteBatch);
                CurrentLife[i].Layer = 1f;
            }

            Rupees = new StaticSprite[2];
            Rupees[0] = new StaticSprite(Game, "0", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            Rupees[0].Layer = 1f;
            Rupees[1] = new StaticSprite(Game, "0", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            Rupees[1].Colour = Color.Black;
            Rupees[1].Layer = 1f;

            Keys = new StaticSprite[2];
            Keys[0] = new StaticSprite(Game, "0", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            Keys[0].Layer = 1f;
            Keys[1] = new StaticSprite(Game, "0", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            Keys[1].Colour = Color.Black;
            Keys[1].Layer = 1f;

            Bombs = new StaticSprite[2];
            Bombs[0] = new StaticSprite(Game, "0", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            Bombs[0].Layer = 1f;
            Bombs[1] = new StaticSprite(Game, "0", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            Bombs[1].Colour = Color.Black;
            Bombs[1].Layer = 1f;

        }
    }
}
