/* Contributors
*  John Nguyen
* Stephen Hogg
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint03
{
    public class Bomb : IAttack
    {
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public StaticSprite Sprite { get; set; }
        public bool CanDamage { get; set; } = false;
        public int Damage { get; set; }
        public States.Direction Direction { get; set; } = States.Direction.None;

        private Game1 Game;
        private IGameObject Creator;
        private int Timer = 0;
        private int Lifespan = 80;
        public Bomb(Game1 game, IGameObject creator)
        {
            Game = game;
            Creator = creator;
            Position = Creator.Position;
            Damage = 0;
        }

        public void Attack()
        {
            Sprite = new StaticSprite(Game, "BombEffect", Position, Game.EffectSpriteSheet, Game.spriteBatch);
            Sprite.FPS = 3;
            Sprite.Layer = 0.25f;
            Game.Dungeon01.Attacks.Add(this);
        }

        public void OnHit()
        {
            //Bomb doesn't do anything on hit
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
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Size.X, (int)Sprite.Size.Y);
            Timer++;
            if (Timer >= Lifespan)
            {
                Detonate();
            }
        }

        private void Detonate()
        {
            Sprite.Colour = Color.Transparent;
            Vector2 startingExplosion = new Vector2(Position.X - 32, Position.Y - 16);

            for(int y = 0; y < 3; y++)
            {
                Vector2 location = startingExplosion;
                location.Y += 16 * y;
                for(int x = 0; x < 3; x++)
                {
                    location.X += 16 * x - 16*(x - 1);
                    IAttack explosion = new Explosion(Game, location, Creator);
                    explosion.Attack();
                }
            }
        }

    }
}
