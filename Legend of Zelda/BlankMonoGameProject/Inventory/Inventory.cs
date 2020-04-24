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
        public Vector2 Size = new Vector2(256, 240);

        private float offset = 12;
        private float miniMapOffsetX = 64;
        private float miniMapOffsetY = 240 - 51;
        private Vector2 mainMapRoomOffset;
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
        public StaticSprite LinkLocationIndicator;
        private int linkLocationIndicatorOffsetFromMapRoom = 2;
        private int linkRoomLocation;

        public StaticSprite[] MapRooms = new StaticSprite[18];
        private Vector2[] MapRoomPositions = new Vector2[18];
        Vector2 updatedMapRoomPos;

        public Inventory(Game1 game)
        {
            Game = game;
            inventoryScreen = Game.Content.Load<Texture2D>("HUD");
            inventoryExtras = Game.Content.Load<Texture2D>("HUD Extras");


            SetupHUDSprites();
            UpdateInventoryCounters();

        }
        public void Draw()
        {
            initialCameraPosition = Game.Camera.Position;
            Vector2 location = new Vector2(initialCameraPosition.X / Game.ScreenScale, (initialCameraPosition.Y -  offset  ) / Game.ScreenScale);

            MainInventory.UpdatePosition(location);
            MainInventory.DrawSprite();

            MiniMap.UpdatePosition(new Vector2((initialCameraPosition.X + miniMapOffsetX) / Game.ScreenScale, ((initialCameraPosition.Y + (miniMapOffsetY * Game.ScreenScale)) / Game.ScreenScale) ));
            MiniMap.DrawSprite();
            MiniMap.Colour = Color.White;

            UpdateInventoryCounters();

            LevelNumber.UpdatePosition(new Vector2(location.X + 64, location.Y + 184));
            LevelNumber.DrawSprite();

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
            for (int i = 0; i < Game.roomsExplored.Length; i++)
            {
                mainMapRoomOffset = MapRoomPositions[i];
                updatedMapRoomPos = new Vector2((initialCameraPosition.X + (mainMapRoomOffset.X * Game.ScreenScale)) / Game.ScreenScale, (initialCameraPosition.Y + (mainMapRoomOffset.Y * Game.ScreenScale)) / Game.ScreenScale);
                MapRooms[i].UpdatePosition(updatedMapRoomPos);
                MapRooms[i].Colour = Color.White;

                if (Game.roomsExplored[i] == 1)
                { 
                    MapRooms[i].DrawSprite();
                }
            }
            linkRoomLocation = int.Parse(Game.CurrDungeon.ActiveRoom.Name.Substring(Game.CurrDungeon.ActiveRoom.Name.Length - 1));
            LinkLocationIndicator.UpdatePosition(new Vector2(MapRooms[linkRoomLocation].Position.X + linkLocationIndicatorOffsetFromMapRoom , MapRooms[linkRoomLocation].Position.Y + linkLocationIndicatorOffsetFromMapRoom));
            LinkLocationIndicator.Colour = Color.White;
            LinkLocationIndicator.DrawSprite();
        }

        public void UpdateInventoryCounters()
        {
            UpdateCurrentHealth(Game.Link.HP);
            UpdateKeyCounter(Game.KeyCounter);
            UpdateRupeeCounter(Game.RupeeCounter);
            UpdateBombCounter(Game.BombCounter);
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
            MiniMap.Colour = Color.White;
        }

        private void SetupHUDSprites()
        {
            MainInventory = new StaticSprite(Game, "HUD", Vector2.Zero, inventoryScreen, Game.spriteBatch);
            MainInventory.Layer = 0.90f;

            MiniMap = new StaticSprite(Game, "Dungeon01Map", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            MiniMap.Layer = 0.91f;
            MiniMap.Colour = Color.Black;

            LifeBar = new StaticSprite[16];
            for(int i = 0; i < LifeBar.Length; i++)
            {
                LifeBar[i] = new StaticSprite(Game, "EmptyHeart", Vector2.Zero, inventoryExtras, Game.spriteBatch);
                LifeBar[i].Layer = 0.92f;
                if(i > 2)
                {
                    LifeBar[i].Colour = Color.Black;
                }
            }

            CurrentLife = new StaticSprite[16];
            for (int i = 0; i < LifeBar.Length; i++)
            {
                CurrentLife[i] = new StaticSprite(Game, "FullHeart", Vector2.Zero, inventoryExtras, Game.spriteBatch);
                CurrentLife[i].Layer = 0.93f;
            }

            Rupees = new StaticSprite[2];
            Rupees[0] = new StaticSprite(Game, "0", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            Rupees[0].Layer = 0.93f;
            Rupees[1] = new StaticSprite(Game, "0", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            Rupees[1].Colour = Color.Black;
            Rupees[1].Layer = 0.93f;

            Keys = new StaticSprite[2];
            Keys[0] = new StaticSprite(Game, "0", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            Keys[0].Layer = 0.93f;
            Keys[1] = new StaticSprite(Game, "0", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            Keys[1].Colour = Color.Black;
            Keys[1].Layer = 0.93f;

            Bombs = new StaticSprite[2];
            Bombs[0] = new StaticSprite(Game, "0", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            Bombs[0].Layer = 0.93f;
            Bombs[1] = new StaticSprite(Game, "0", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            Bombs[1].Colour = Color.Black;
            Bombs[1].Layer = 0.93f;

            LevelNumber = new StaticSprite(Game, "1", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            LevelNumber.Layer = 0.93f;

            LinkLocationIndicator = new StaticSprite(Game, "LinkLocationMap", Vector2.Zero, inventoryExtras, Game.spriteBatch);
            LinkLocationIndicator.Layer = 0.94f;

            //this is actually room 17 in accordance with group numbering standard, all others as are they say
            MapRooms[0] = new StaticSprite(Game, "MapRoomTypeC", Vector2.Zero, inventoryExtras, Game.spriteBatch)
            {
                Colour = Color.White,
                Layer = 0.93f,
                TotalFrames = 1
            };
            MapRoomPositions[0] = new Vector2(152, 112);

            MapRooms[1] = new StaticSprite(Game, "MapRoomTypeI", Vector2.Zero, inventoryExtras, Game.spriteBatch)
            {
                Colour = Color.White,
                Layer = 0.93f,
                TotalFrames = 1
            };
            MapRoomPositions[1] = new Vector2(144, 152);

            MapRooms[2] = new StaticSprite(Game, "MapRoomTypeE", Vector2.Zero, inventoryExtras, Game.spriteBatch)
            {
                Colour = Color.White,
                Layer = 0.93f,
                TotalFrames = 1
            };
            MapRoomPositions[2] = new Vector2(152, 152);
            

            MapRooms[3] = new StaticSprite(Game, "MapRoomTypeA", Vector2.Zero, inventoryExtras, Game.spriteBatch)
            {
                Colour = Color.White,
                Layer = 0.93f,
                TotalFrames = 1
            };
            MapRoomPositions[3] = new Vector2(160, 152);
            

            MapRooms[4] = new StaticSprite(Game, "MapRoomTypeN", Vector2.Zero, inventoryExtras, Game.spriteBatch)
            {
                Colour = Color.White,
                Layer = 0.93f,
                TotalFrames = 1
            };
            MapRoomPositions[4] = new Vector2(152, 144);
            

            MapRooms[5] = new StaticSprite(Game, "MapRoomTypeJ", Vector2.Zero, inventoryExtras, Game.spriteBatch)
            {
                Colour = Color.White,
                Layer = 0.93f,
                TotalFrames = 1
            };
            MapRoomPositions[5] = new Vector2(144, 136);


            MapRooms[6] = new StaticSprite(Game, "MapRoomTypeH", Vector2.Zero, inventoryExtras, Game.spriteBatch)
            {
                Colour = Color.White,
                Layer = 0.93f,
                TotalFrames = 1
            };
            MapRoomPositions[6] = new Vector2(152, 136);
            

            MapRooms[7] = new StaticSprite(Game, "MapRoomTypeD", Vector2.Zero, inventoryExtras, Game.spriteBatch)
            {
                Colour = Color.White,
                Layer = 0.93f,
                TotalFrames = 1
            };
            MapRoomPositions[7] = new Vector2(160, 136);
            

            MapRooms[8] = new StaticSprite(Game, "MapRoomTypeI", Vector2.Zero, inventoryExtras, Game.spriteBatch)
            {
                Colour = Color.White,
                Layer = 0.93f,
                TotalFrames = 1
            };
            MapRoomPositions[8] = new Vector2(136, 128);
            

            MapRooms[9] = new StaticSprite(Game, "MapRoomTypeF", Vector2.Zero, inventoryExtras, Game.spriteBatch)
            {
                Colour = Color.White,
                Layer = 0.93f,
                TotalFrames = 1
            };
            MapRoomPositions[9] = new Vector2(144, 128);
            

            MapRooms[10] = new StaticSprite(Game, "MapRoomTypeH", Vector2.Zero, inventoryExtras, Game.spriteBatch)
            {
                Colour = Color.White,
                Layer = 0.93f,
                TotalFrames = 1
            };
            MapRoomPositions[10] = new Vector2(152, 128);
            

            MapRooms[11] = new StaticSprite(Game, "MapRoomTypeF", Vector2.Zero, inventoryExtras, Game.spriteBatch)
            {
                Colour = Color.White,
                Layer = 0.93f,
                TotalFrames = 1
            };
            MapRoomPositions[11] = new Vector2(160, 128);
            

            MapRooms[12] = new StaticSprite(Game, "MapRoomTypeD", Vector2.Zero, inventoryExtras, Game.spriteBatch)
            {
                Colour = Color.White,
                Layer = 0.93f,
                TotalFrames = 1
            };
            MapRoomPositions[12] = new Vector2(168, 128);
            

            MapRooms[13] = new StaticSprite(Game, "MapRoomTypeN", Vector2.Zero, inventoryExtras, Game.spriteBatch)
            {
                Colour = Color.White,
                Layer = 0.93f,
                TotalFrames = 1
            };
            MapRoomPositions[13] = new Vector2(152, 120);
            

            MapRooms[14] = new StaticSprite(Game, "MapRoomTypeK", Vector2.Zero, inventoryExtras, Game.spriteBatch)
            {
                Colour = Color.White,
                Layer = 0.93f,
                TotalFrames = 1
            };
            MapRoomPositions[14] = new Vector2(168, 120);
            

            MapRooms[15] = new StaticSprite(Game, "MapRoomTypeA", Vector2.Zero, inventoryExtras, Game.spriteBatch)
            {
                Colour = Color.White,
                Layer = 0.93f,
                TotalFrames = 1
            };
            MapRoomPositions[15] = new Vector2(176, 120);
            

            MapRooms[16] = new StaticSprite(Game, "MapRoomTypeI", Vector2.Zero, inventoryExtras, Game.spriteBatch)
            {
                Colour = Color.White,
                Layer = 0.93f,
                TotalFrames = 1
            };
            MapRoomPositions[16] = new Vector2(144, 112);


            MapRooms[17] = new StaticSprite(Game, "MapRoomTypeC", Vector2.Zero, inventoryExtras, Game.spriteBatch)
            {
                Colour = Color.White,
                Layer = 0.93f,
                TotalFrames = 1,
                
            };
            MapRoomPositions[17] = new Vector2(152, 112);
            
        }
    }
}
