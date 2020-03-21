/* Contributors
* Stephen Hogg
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Sprint03
{
    public class MovableBlock : IGameObject
    {
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public StaticSprite Sprite { get; set; }

        public bool TriggerDoor = false;

        private States.Direction MovableDirection;
        private float MovableDistance = 16;
        private Vector2 MoveSpeed;

        public MovableBlock(Game1 game, Vector2 position, States.Direction movableDirection)
        {
            Position = position;
            Sprite = new StaticSprite(game, "Block", position, game.TileSpriteSheet, game.spriteBatch);
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Size.X, (int)Sprite.Size.Y);
            MovableDirection = movableDirection;
            SetSpeed();
        }

        public void PushBlock(States.Direction directionPushed)
        {
            if (directionPushed.Equals(MovableDirection) && MovableDistance != 0)
            {
                Sprite.UpdatePosition(Position);
                Position += MoveSpeed;
                MovableDistance -= 0.5f;
            }
            else if (MovableDistance == 0)
            {
                TriggerDoor = true;
            }
        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }

        public void Update()
        {
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Size.X, (int)Sprite.Size.Y);
        }

        private void SetSpeed()
        {
            MoveSpeed = Vector2.Zero;

            switch (MovableDirection)
            {
                case States.Direction.Up:
                    MoveSpeed.X = 0;
                    MoveSpeed.Y = -0.5f;
                    break;
                case States.Direction.Down:
                    MoveSpeed.X = 0;
                    MoveSpeed.Y = 0.5f;
                    break;
                case States.Direction.Left:
                    MoveSpeed.X = -0.5f;
                    MoveSpeed.Y = 0;
                    break;
                case States.Direction.Right:
                    MoveSpeed.X = 0.5f;
                    MoveSpeed.Y = 0;
                    break;
            }
        }
    }
}
