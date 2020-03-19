using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint03
{
    public class Sword : IAttack
    {
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public StaticSprite Sprite { get; set; }
        public bool CanDamage { get; set; } = true;
        public int Damage { get; set; }
        public States.Direction Direction { get; set; }

        private Game1 Game;
        private ILink Creator;
        private int Timer = 0;
        private int Lifespan = 10;
        public Sword(Game1 game, ILink creator)
        {
            Game = game;
            Creator = creator;
            Position = Creator.Position;
            Damage = 1;
        }

        public void Attack()
        {
            Tuple<string, SpriteEffects> swordInfo = SwordSpriteHelper();
            Sprite = new StaticSprite(Game, swordInfo.Item1, Position, Game.EffectSpriteSheet, Game.spriteBatch);
            Sprite.SpriteEffect = swordInfo.Item2;
            Sprite.Layer = 0.25f;
            Sprite.FPS = 12;
            Game.Dungeon01.Attacks.Add(this);
        }

        public void OnHit()
        {
            //Sword doesn't destroy itself on hit
        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }

        public bool IsCreator(IGameObject obj)
        {
            return obj.Equals(Creator);
        }

        public void Update()
        {
            Timer++;
            if(Timer >= Lifespan)
            {
                Sprite.Colour = Color.Transparent;
            }
            Sprite.UpdatePosition(Position);
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Size.X, (int)Sprite.Size.Y);
        }

        // Places the hilt of the sword sprite in Link's hand
        private Tuple<string, SpriteEffects> SwordSpriteHelper()
        {
            string name = "";
            SpriteEffects sEffect= SpriteEffects.None;
            switch (Creator.Direction)
            {
                case (States.Direction.Down):
                    Position = new Vector2(Creator.Position.X + 6, Creator.Position.Y + 12);
                    name = "RedLightsaber";
                    sEffect = SpriteEffects.FlipVertically;
                    break;

                case (States.Direction.Up):
                    Position = new Vector2(Creator.Position.X + 3, Creator.Position.Y - 12);
                    name = "RedLightsaber";
                    break;

                case (States.Direction.Left):
                    Position = new Vector2(Creator.Position.X - 12, Creator.Position.Y + 6);
                    name = "RedLightsaberHorizontal";
                    break;

                case (States.Direction.Right):
                    Position = new Vector2(Creator.Position.X + 12, Creator.Position.Y + 6);
                    name = "RedLightsaberHorizontal";
                    sEffect = SpriteEffects.FlipHorizontally;
                    break;

                default:
                    break;
            }
            return new Tuple<string, SpriteEffects>(name, sEffect);
        }
    }
}
