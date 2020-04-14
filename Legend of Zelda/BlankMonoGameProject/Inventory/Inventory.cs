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

        public StaticSprite[] MapRooms = new StaticSprite[18];

        public Inventory(Game1 game)
        {
            Game = game;
            Size = new Vector2(256, 240);
            inventoryScreen = Game.Content.Load<Texture2D>("HUD");
            inventoryExtras = Game.Content.Load<Texture2D>("HUD Extras");

            SetupHUDSprites();
            UpdateCurrentHealth(Game.Link.HP);
            UpdateKeyCounter(Game.KeyCounter);
            UpdateRupeeCounter(Game.RupeeCounter);
            UpdateBombCounter(Game.BombCounter);

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

            // Inventory Map Draw and Update loop
            Vector2 updatedMapRoomPos;
            for (int i = 0; i < Game.roomsExplored.Length; i++)
            {
                if (Game.roomsExplored[i] == 1)
                {
                    updatedMapRoomPos = MapRooms[i].Position;
                    updatedMapRoomPos += location;
                    MapRooms[i].UpdatePosition(updatedMapRoomPos);
                    MapRooms[i].DrawSprite();
                }
            }
        }

        public void UpdateRupeeCounter(int totalRupees)
        {
            if (totalRupees > 9)
            {
                int onesPlace = totalRupees % 10;
                int tensPlaces = totalRupees / 10;
                Rupees[0].ChangeSpriteAnimation(tensPlaces.ToString());
                Rupees[1].ChangeSpriteAnimation(onesPlace.ToString());
                Rupees[1].Colour = Color.White;
            }
            else
            {
                Rupees[0].ChangeSpriteAnimation(totalRupees.ToString());
                Rupees[1].Colour = Color.Black;
            }
        }

        public void UpdateBombCounter(int totalBombs)
        {
            if (totalBombs > 9)
            {
                int onesPlace = totalBombs % 10;
                int tensPlaces = totalBombs / 10;
                Bombs[0].ChangeSpriteAnimation(tensPlaces.ToString());
                Bombs[1].ChangeSpriteAnimation(onesPlace.ToString());
                Bombs[1].Colour = Color.White;
            }
            else
            {
                Bombs[0].ChangeSpriteAnimation(totalBombs.ToString());
                Bombs[1].Colour = Color.Black;
            }
        }

        public void UpdateKeyCounter(int totalKeys)
        {
            if (totalKeys > 9)
            {
                int onesPlace = totalKeys % 10;
                int tensPlaces = totalKeys / 10;
                Keys[0].ChangeSpriteAnimation(tensPlaces.ToString());
                Keys[1].ChangeSpriteAnimation(onesPlace.ToString());
                Keys[1].Colour = Color.White;
            }
            else
            {
                Keys[0].ChangeSpriteAnimation(totalKeys.ToString());
                Keys[1].Colour = Color.Black;
            }
        }

        public void UpdateCurrentHealth(int hp)
        {
            for (int i = 0; i < CurrentLife.Length; i++)
            {
                if (i < hp / 2)
                {
                    CurrentLife[i].ChangeSpriteAnimation("FullHeart");

                }
                else if (hp % 2 != 0 && i == hp / 2)
                {
                    CurrentLife[i].ChangeSpriteAnimation("HalfHeart");
                }
                else
                {
                    CurrentLife[i].ChangeSpriteAnimation("EmptyHeart");

                }
            }
        }

        public void UpdateLifeBar(int maxHP)
        {
            LifeBar[maxHP / 2 - 1].Colour = Color.White;
        }

        public void UpdateMap()
        {
            Map.Colour = Color.White;
        }

        private void SetupHUDSprites()
        {
            MainInventory = new StaticSprite(Game, "HUD", Vector2.Zero, inventoryScreen, Game.spriteBatch);
            MainInventory.Layer = 0.95f;

            MiniMap = new StaticSprite(Game, "Dungeon01Map", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MiniMap.Layer = .96f;
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


            MapRooms[0] = new StaticSprite(Game, "MapRoomTypeO", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MapRooms[0].Layer = 1f;
            MapRooms[0].TotalFrames = 1;
            MapRooms[0].UpdatePosition(new Vector2(152, 160));

            MapRooms[1] = new StaticSprite(Game, "MapRoomTypeI", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MapRooms[1].Layer = 1f;
            MapRooms[1].TotalFrames = 1;
            MapRooms[1].UpdatePosition(new Vector2(144, 152));

            MapRooms[2] = new StaticSprite(Game, "MapRoomTypeH", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MapRooms[2].Layer = 1f;
            MapRooms[2].TotalFrames = 1;
            MapRooms[2].UpdatePosition(new Vector2(152, 152));

            MapRooms[3] = new StaticSprite(Game, "MapRoomTypeA", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MapRooms[3].Layer = 1f;
            MapRooms[3].TotalFrames = 1;
            MapRooms[3].UpdatePosition(new Vector2(160, 152));

            MapRooms[4] = new StaticSprite(Game, "MapRoomTypeN", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MapRooms[4].Layer = 1f;
            MapRooms[4].TotalFrames = 1;
            MapRooms[4].UpdatePosition(new Vector2(152, 144));

            MapRooms[5] = new StaticSprite(Game, "MapRoomTypeJ", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MapRooms[5].Layer = 1f;
            MapRooms[5].TotalFrames = 1;
            MapRooms[5].UpdatePosition(new Vector2(144, 136));

            MapRooms[6] = new StaticSprite(Game, "MapRoomTypeH", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MapRooms[6].Layer = 1f;
            MapRooms[6].TotalFrames = 1;
            MapRooms[6].UpdatePosition(new Vector2(152, 136));

            MapRooms[7] = new StaticSprite(Game, "MapRoomTypeD", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MapRooms[7].Layer = 1f;
            MapRooms[7].TotalFrames = 1;
            MapRooms[7].UpdatePosition(new Vector2(160, 136));

            MapRooms[8] = new StaticSprite(Game, "MapRoomTypeI", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MapRooms[8].Layer = 1f;
            MapRooms[8].TotalFrames = 1;
            MapRooms[8].UpdatePosition(new Vector2(136, 128));

            MapRooms[9] = new StaticSprite(Game, "MapRoomTypeF", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MapRooms[9].Layer = 1f;
            MapRooms[9].TotalFrames = 1;
            MapRooms[9].UpdatePosition(new Vector2(144, 128));

            MapRooms[10] = new StaticSprite(Game, "MapRoomTypeH", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MapRooms[10].Layer = 1f;
            MapRooms[10].TotalFrames = 1;
            MapRooms[10].UpdatePosition(new Vector2(152, 128));

            MapRooms[11] = new StaticSprite(Game, "MapRoomTypeF", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MapRooms[11].Layer = 1f;
            MapRooms[11].TotalFrames = 1;
            MapRooms[11].UpdatePosition(new Vector2(160, 128));

            MapRooms[12] = new StaticSprite(Game, "MapRoomTypeD", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MapRooms[12].Layer = 1f;
            MapRooms[12].TotalFrames = 1;
            MapRooms[12].UpdatePosition(new Vector2(168, 128));

            MapRooms[13] = new StaticSprite(Game, "MapRoomTypeN", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MapRooms[13].Layer = 1f;
            MapRooms[13].TotalFrames = 1;
            MapRooms[13].UpdatePosition(new Vector2(152, 120));

            MapRooms[14] = new StaticSprite(Game, "MapRoomTypeK", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MapRooms[14].Layer = 1f;
            MapRooms[14].TotalFrames = 1;
            MapRooms[14].UpdatePosition(new Vector2(168, 120));

            MapRooms[15] = new StaticSprite(Game, "MapRoomTypeA", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MapRooms[15].Layer = 1f;
            MapRooms[15].TotalFrames = 1;
            MapRooms[15].UpdatePosition(new Vector2(176, 120));

            MapRooms[16] = new StaticSprite(Game, "MapRoomTypeI", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MapRooms[16].Layer = 1f;
            MapRooms[16].TotalFrames = 1;
            MapRooms[16].UpdatePosition(new Vector2(144, 112));

            MapRooms[17] = new StaticSprite(Game, "MapRoomTypeC", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MapRooms[17].Layer = 1f;
            MapRooms[17].TotalFrames = 1;
            MapRooms[17].UpdatePosition(new Vector2(152, 112));
        }
    }
}
