/* Contributors
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Sprint03
{
    public class HUD
    {
        private Game1 Game;
        private readonly Texture2D hud;
        private readonly Texture2D hudExtras;
        public Vector2 Size;
        private Vector2 miniMapPosition;

        public StaticSprite MainHUD;
        public StaticSprite Map;
        public StaticSprite WeaponA;
        public StaticSprite WeaponB;
        public StaticSprite[] LifeBar;
        public StaticSprite[] CurrentLife;
        public StaticSprite[] Rupees;
        public StaticSprite[] Keys;
        public StaticSprite[] Bombs;
        public StaticSprite LevelNumber;

        public HUD(Game1 game)
        {
            Game = game;
            Size = new Vector2(256, 64);
            hud = Game.Content.Load<Texture2D>("HUD");
            hudExtras = Game.Content.Load<Texture2D>("HUD Extras");
            SetupHUDSprites();
            UpdateCurrentHealth(Game.Link.HP);
            UpdateKeyCounter(Game.KeyCounter);
            UpdateRupeeCounter(Game.RupeeCounter);
            UpdateBombCounter(Game.BombCounter);
        }
        public void Draw()
        {
            Vector2 loc = new Vector2(Game.Camera.Position.X / Game.ScreenScale, (Game.Camera.Position.Y - 176 * Game.ScreenScale) / Game.ScreenScale);
            MainHUD.UpdatePosition(loc);
            MainHUD.DrawSprite();

            miniMapPosition = new Vector2(Game.Camera.Position.X / Game.ScreenScale + 16, ((Game.Camera.Position.Y - Game.ScreenScale * 176f) / Game.ScreenScale) + 192);
            Map.UpdatePosition(miniMapPosition);
            Map.DrawSprite();


            WeaponA.UpdatePosition(new Vector2(loc.X + 152f, loc.Y + 208f));
            WeaponA.DrawSprite();

            LevelNumber.UpdatePosition(new Vector2(miniMapPosition.X + 48, miniMapPosition.Y - 8));
            LevelNumber.DrawSprite();

            // Item Draw and Update loop
            for (int i = 0; i < Rupees.Length; i++)
            {
                Rupees[i].UpdatePosition(new Vector2(loc.X + 104 + 8 * i, loc.Y + 200));
                Keys[i].UpdatePosition(new Vector2(loc.X + 104 + 8 * i, loc.Y + 216));
                Bombs[i].UpdatePosition(new Vector2(loc.X + 104 + 8 * i, loc.Y + 224));
                Rupees[i].DrawSprite();
                Keys[i].DrawSprite();
                Bombs[i].DrawSprite();
            }

            // Health Draw and Update loop
            for (int i = 0; i < LifeBar.Length; i++)
            {
                if (i < 8)
                {
                    LifeBar[i].UpdatePosition(new Vector2(loc.X + 176 + 8 * i, loc.Y + 216));
                    CurrentLife[i].UpdatePosition(new Vector2(loc.X + 176 + 8 * i, loc.Y + 216));

                }
                else
                {
                    LifeBar[i].UpdatePosition(new Vector2(loc.X + 176 + 8 * (i - 8), loc.Y + 224));
                    CurrentLife[i].UpdatePosition(new Vector2(loc.X + 176 + 8 * (i - 8), loc.Y + 224));



                }
                LifeBar[i].DrawSprite();
                if (!LifeBar[i].Colour.Equals(Color.Black)) {
                    CurrentLife[i].DrawSprite();
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
                else if (hp % 2 != 0 && i == hp/2)
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

        public void HideHud()
        {
            MainHUD.Colour = Color.Transparent;
            WeaponA.Colour = Color.Transparent;
            Map.Colour = Color.Transparent;

            for (int i = 0; i < Rupees.Length; i++)
            {
                Rupees[i].Colour = Color.Transparent;
                Keys[i].Colour = Color.Transparent;
                Bombs[i].Colour = Color.Transparent;
            }

            for(int i = 0; i < LifeBar.Length; i++)
            {
                LifeBar[i].Colour = Color.Transparent;
                CurrentLife[i].Colour = Color.Transparent;
            }
            
        }

        private void SetupHUDSprites()
        {
            MainHUD = new StaticSprite(Game, "HUD", Vector2.Zero, hud, Game.spriteBatch);
            MainHUD.Layer = 0.95f;

            Map = new StaticSprite(Game, "Dungeon01Map", Vector2.Zero, hudExtras, Game.spriteBatch);
            Map.Layer = 1f;
            Map.Colour = Color.Black;

            WeaponA = new StaticSprite(Game, "SwordSwing", Vector2.Zero, Game.EffectSpriteSheet, Game.spriteBatch);
            WeaponA.Layer = 1f;
            WeaponA.TotalFrames = 1;

            LifeBar = new StaticSprite[16];
            for(int i = 0; i < LifeBar.Length; i++)
            {
                LifeBar[i] = new StaticSprite(Game, "EmptyHeart", Vector2.Zero, hudExtras, Game.spriteBatch);
                LifeBar[i].Layer = 0.98f;
                if(i > 2)
                {
                    LifeBar[i].Colour = Color.Black;
                }
            }

            CurrentLife = new StaticSprite[16];
            for (int i = 0; i < LifeBar.Length; i++)
            {
                CurrentLife[i] = new StaticSprite(Game, "FullHeart", Vector2.Zero, hudExtras, Game.spriteBatch);
                CurrentLife[i].Layer = 1f;
            }

            Rupees = new StaticSprite[2];
            Rupees[0] = new StaticSprite(Game, "0", Vector2.Zero, hudExtras, Game.spriteBatch);
            Rupees[0].Layer = 1f;
            Rupees[1] = new StaticSprite(Game, "0", Vector2.Zero, hudExtras, Game.spriteBatch);
            Rupees[1].Colour = Color.Black;
            Rupees[1].Layer = 1f;

            Keys = new StaticSprite[2];
            Keys[0] = new StaticSprite(Game, "0", Vector2.Zero, hudExtras, Game.spriteBatch);
            Keys[0].Layer = 1f;
            Keys[1] = new StaticSprite(Game, "0", Vector2.Zero, hudExtras, Game.spriteBatch);
            Keys[1].Colour = Color.Black;
            Keys[1].Layer = 1f;

            Bombs = new StaticSprite[2];
            Bombs[0] = new StaticSprite(Game, "0", Vector2.Zero, hudExtras, Game.spriteBatch);
            Bombs[0].Layer = 1f;
            Bombs[1] = new StaticSprite(Game, "0", Vector2.Zero, hudExtras, Game.spriteBatch);
            Bombs[1].Colour = Color.Black;
            Bombs[1].Layer = 1f;

            LevelNumber = new StaticSprite(Game, "1", Vector2.Zero, hudExtras, Game.spriteBatch);
            LevelNumber.Layer = 1f;
        }
    }
}
