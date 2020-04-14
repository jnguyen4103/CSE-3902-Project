/* Contributors
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint03
{
    public class Boomerang : IAttack
    {
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public StaticSprite Sprite { get; set; }
        public bool CanDamage { get; set; } = true;
        public int Damage { get; set; }
        public States.Direction Direction;

        private Game1 Game;
        private IGameObject Creator;
        private Vector2 velocity;
        private Vector2 slowdown;
        private bool returning = false;
        private bool madeContact = false;
        private int Timer = 0;
        private int ReturnDelay = 90;
        public Boomerang(Game1 game, IGameObject creator, States.Direction direction)
        {
            Game = game;
            Creator = creator;
            Position = Creator.Position;
            Direction = direction;
            Damage = 1;
        }

        public void Attack()
        {
            SetupAttack();
            Game.CurrDungeon.Attacks.Add(this);
        }

        public void OnHit()
        {
            Timer = 0;
            CanDamage = false;
            velocity = Vector2.Zero;
            returning = true;
            madeContact = true;
            Sprite.ChangeSpriteAnimation("ProjectileHit");
        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }

        public bool IsCreator(IGameObject obj)
        {
            return obj.Hitbox.Equals(Creator.Hitbox);

        }

        public void Update()
        {
            if(Timer < ReturnDelay)
            {
                Timer++;
            }

            if (!returning && Timer < ReturnDelay)
            {
                velocity += slowdown;
            }
            else if(Timer == ReturnDelay && !madeContact)
            {
                CanDamage = false;
                returning = true;
            }
            else if (Timer == 4 && madeContact)
            {
                Timer = ReturnDelay;
                Sprite.ChangeSpriteAnimation("BoomerangEffect");
            }
            if(returning)
            {
                Return();
            }
                
            Position += velocity;
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Size.X, (int)Sprite.Size.Y);
            Sprite.UpdatePosition(Position);
        }

        private void SetupAttack()
        {
            velocity = Vector2.Zero;
            slowdown = Vector2.Zero;
            Vector2 offset = Vector2.Zero;

            switch (Direction)
            {
                case (States.Direction.Down):
                    offset.X += 4;
                    offset.Y += 14;
                    velocity.Y = 3f;
                    slowdown.Y = -0.06f;
                    break;
                case (States.Direction.Up):
                    offset.X += 4;
                    offset.Y -= 4;
                    velocity.Y = -3f;
                    slowdown.Y = 0.06f;
                    break;
                case (States.Direction.Left):
                    offset.X -= 14;
                    offset.Y += 4;
                    velocity.X = -3f;
                    slowdown.X = 0.06f;
                    break;
                case (States.Direction.Right):
                    offset.X += 14;
                    offset.Y += 4;
                    velocity.X = 3f;
                    slowdown.X  = -0.06f;
                    break;
                default:
                    break;
            }

            Position += offset;
            Sprite = new StaticSprite(Game, "BoomerangEffect", Position, Game.EffectSpriteSheet, Game.spriteBatch);
            Sprite.FPS = 20;
            Sprite.Layer = 0.55f;
        }

        private void Return()
        {
            velocity = Vector2.Zero;
            if(Math.Abs((Position.X + Sprite.Size.X/2) - (Creator.Position.X + Creator.Hitbox.Size.X/2)) > Sprite.Size.X / 2)
            {
                if ((Position.X + (Sprite.Size.X / 2)) > Creator.Position.X + (Creator.Hitbox.Size.X / 2)) { velocity.X -= 1.5f; }
                if ((Position.X + (Sprite.Size.X / 2)) < Creator.Position.X + (Creator.Hitbox.Size.X / 2)) { velocity.X += 1.5f; }
            }
            if (Math.Abs((Position.Y + Sprite.Size.Y / 2) - (Creator.Position.Y + Creator.Hitbox.Size.Y / 2)) > Sprite.Size.Y / 2)
            {
                if ((Position.Y + (Sprite.Size.Y / 2)) > Creator.Position.Y + (Creator.Hitbox.Size.Y / 2)) { velocity.Y -= 1.5f; }
                if ((Position.Y + (Sprite.Size.Y / 2)) < Creator.Position.Y + (Creator.Hitbox.Size.Y / 2)) { velocity.Y += 1.5f; }
            }
        }

    }
}
