/* Contributors
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint03
{
    public class BlueCandle : IAttack
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
        private int Timer = 0;
        private int TravelDistance = 64;
        private int Lifespan = 300;
        /*
* addes sound Effects
* 0.LOZ_Arrow_Boomerang
* 1 LOZ_Bomb_Blow
* 2 LOZ_Bomb_Drop
* 3 LOZ_Boss_Scream1
* 4 LOZ_Candle
* 5 LOZ_Door_Unlock
* 6 LOZ_Enemy_Die
* 7 LOZ_Enemy_Hit
* 8 LOZ_Fanfare
* 9 LOZ_Get_Heart
* 10 LOZ_Get_Item
* 11 LOZ_Get_Rupee
* 12 LOZ_Key_Appear
* 13 LOZ_Link_Die
* 14 LOZ_Link_Hurt
* 15 LOZ_Secret
* 16 LOZ_Stairs
* 17 LOZ_Sword_Shoot
* 18 LOZ_Sword_Slash
*/
        public BlueCandle(Game1 game, IGameObject creator, States.Direction direction)
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
            Game.Dungeon01.Attacks.Add(this);
        }

        public void OnHit()
        {
            // Does nothing but damage
        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }

        public bool IsCreator(IGameObject obj)
        {
            // Doesn't care who it's creator it, it damages everything
            return false;
        }

        public void Update()
        {
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Size.X, (int)Sprite.Size.Y);
            Timer++;
            if(Timer < TravelDistance)
            {
                Position += velocity;
                Sprite.UpdatePosition(Position);

            }
            else if (Timer >= Lifespan)
            {
                Sprite.Colour = Color.Transparent;
            }
        }

        private void SetupAttack()
        {
            velocity = Vector2.Zero;
            Vector2 offset = Vector2.Zero;

            switch (Direction)
            {
                case (States.Direction.Down):
                    offset.Y += 16;
                    velocity.Y = 0.25f;
                    break;
                case (States.Direction.Up):
                    offset.Y -= 16;
                    velocity.Y = -0.25f;
                    break;
                case (States.Direction.Left):
                    offset.X -= 16;
                    velocity.X = -0.25f;
                    break;
                case (States.Direction.Right):
                    offset.X += 16;
                    velocity.X = 0.25f;
                    break;
                default:
                    break;
            }

            Position += offset;
            Sprite = new StaticSprite(Game, "FireEffect", Position, Game.EffectSpriteSheet, Game.spriteBatch);
            Sprite.Layer = 0.25f;
        }

    }
}
