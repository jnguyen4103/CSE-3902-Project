/* Contributors
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
    public class Explosion : IAttack
    {
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public StaticSprite Sprite { get; set; }
        public bool CanDamage { get; set; } = true;
        public int Damage { get; set; }
        public States.Direction Direction { get; set; } = States.Direction.None;

        private Game1 Game;
        private IGameObject Creator;
        private int Timer = 0;
        private int DamageDuration = 15;
        private int Lifespan = 85;
        public Explosion(Game1 game, Vector2 position, IGameObject creator)
        {
            Game = game;
            Creator = creator;
            Position = position;
            Damage = 2;

        }

        public void Attack()
        {
            Sprite = new StaticSprite(Game, "GreyExplosionEffect", Position, Game.EffectSpriteSheet, Game.spriteBatch);
            Sprite.FPS = 2;
            Sprite.Layer = 0.9f;
            Game.Dungeon01.Attacks.Add(this);
        }

        public void OnHit()
        {
            // Doesn't do anything on contact other than damage
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

            if (Timer == DamageDuration)
            {
                CanDamage = false;
            }

            if (Timer >= Lifespan)
            {
                Sprite.Colour = Color.Transparent;
            }
        }

    }
}
