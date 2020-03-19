using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    class BladeTrap : ITrap
    {
        public StaticSprite Sprite { get; set; }
        public int Damage { get; set; } = 2;
        public bool CanDamage { get; set; } = true;
        public Rectangle Hitbox { get ; set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        private bool isActive = true;
        public Vector2 Velocity = new Vector2(0, 0);

        Game1 Game;

        public BladeTrap(Game1 game, Vector2 spawn)
        {
            Game = game;
            Sprite = new StaticSprite(game, "BladeTrap", spawn, game.MonsterSpriteSheet, game.spriteBatch);
            Position = spawn;
            Origin = Position;
        }

        private void moveToHorizontalCenter(Vector2  velocity)
        {
            Vector2 middle = (velocity.X > 0) ? new Vector2( Origin.X + 80,Origin.Y) : new Vector2(Origin.X - 80, Origin.Y);
            if(Position ==  middle )
            {
                Resetting();
                isActive = false;
            }
            else
            {
                Position += velocity;
            }
        }

        private void moveVerticallyToCenter(Vector2 velocity)
        {
            Vector2 middle = (velocity.Y > 0) ? new Vector2(Origin.X , Origin.Y+40) : new Vector2(Origin.X, Origin.Y-40);
            if (Position == middle)
            {
                Resetting();
                isActive = false;
            }
            else
            {
                Position += velocity;
            }
        }

        public void Active()
        {

            /*
            * Checks if Link is under the top left BladeTrap
            */
            bool underNeathTopLeft = (Origin.X + 16 >= Game.Link.Position.X) && (Origin.X + 16 <= Game.Link.Position.X + 16) &&
                                (Origin.Y <= Game.Link.Position.Y) && (Origin.Y + 16 <= Game.Link.Position.Y+16);
           
            bool aboveBottomLeft = (Origin.X + 16 >= Game.Link.Position.X) && (Origin.X + 16 <= Game.Link.Position.X + 16) &&
                            (Origin.Y >= Game.Link.Position.Y) && (Origin.Y + 16 >= Game.Link.Position.Y);

            bool rightOfBottomLeft = (Origin.X <= Game.Link.Position.X) &&(Origin.X+16 <= Game.Link.Position.X+16) &&
                                      (Origin.Y >= Game.Link.Position.Y) && (Origin.Y +16<= Game.Link.Position.Y+16);

            bool rightOfTopLeft = (Origin.X <= Game.Link.Position.X) && (Origin.X + 16 <= Game.Link.Position.X + 16) &&
                                      (Origin.Y <= Game.Link.Position.Y+16) && (Origin.Y +16 >= Game.Link.Position.Y);



            bool underNeathTopRight = (Origin.X <= Game.Link.Position.X + 16) && (Origin.X + 16 >= Game.Link.Position.X + 16) &&
                                (Origin.Y <= Game.Link.Position.Y) && (Origin.Y + 16 <= Game.Link.Position.Y + 16);
            bool leftOfTopRight = (Origin.X >= Game.Link.Position.X) && (Origin.X + 16 >= Game.Link.Position.X + 16) &&
                                      (Origin.Y <= Game.Link.Position.Y + 16) && (Origin.Y + 16 >= Game.Link.Position.Y);
            bool aboveBottomRight = (Origin.X <= Game.Link.Position.X+16) && (Origin.X + 16 >= Game.Link.Position.X + 16) &&
                            (Origin.Y >= Game.Link.Position.Y) && (Origin.Y + 16 >= Game.Link.Position.Y);

            bool leftOfBottomRIght = (Origin.X >= Game.Link.Position.X) && (Origin.X + 16 >= Game.Link.Position.X + 16) &&
                                      (Origin.Y >= Game.Link.Position.Y) && (Origin.Y + 16 <= Game.Link.Position.Y + 16); ;
            if (isActive)
            {
                if (aboveBottomLeft && !rightOfBottomLeft)
                {
                    Velocity.X = 0;
                    Velocity.Y = -1f;
                    moveVerticallyToCenter(Velocity);
                }
                else if (underNeathTopLeft)
                {
                    Velocity.X = 0;
                    Velocity.Y = 1f;
                    moveVerticallyToCenter(Velocity);
                }
                else if (rightOfBottomLeft)
                {
                    Velocity.X = 1;
                    Velocity.Y = 0;
                    moveToHorizontalCenter(Velocity);

                }
                else if (rightOfTopLeft && !underNeathTopLeft)
                {
                    Velocity.X = 1;
                    Velocity.Y = 0;
                    moveToHorizontalCenter(Velocity);

                }
                else if (underNeathTopRight)
                {
                    Velocity.X = 0;
                    Velocity.Y = 1f;
                    moveVerticallyToCenter(Velocity);
                }
                else if (leftOfTopRight)
                {
                    Velocity.Y = 0;
                    Velocity.X = -1f;
                    moveToHorizontalCenter(Velocity);

                }
                else if (aboveBottomRight)
                {
                    Velocity.Y = -1;
                    Velocity.X = 0;
                    moveVerticallyToCenter(Velocity);
                }
                else if (rightOfBottomLeft)
                {
                    Velocity.Y = 0;
                    Velocity.X = 1f;
                    moveToHorizontalCenter(Velocity);
                }
                else
                {
                   if(Velocity.X !=0)
                    {
                        moveToHorizontalCenter(Velocity);
                    }
                    else
                    {
                        moveVerticallyToCenter(Velocity);
                    }
                }
            }
        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }

        public void Inactive()
        {
            /*Waitng*/
            Resetting();
        }

        public void OnHit()
        {

        }

        public void Removed()
        {
            Sprite.Colour = Color.Transparent;
        }

        public void Resetting()
        { 
            if(Origin.X != Position.X)
            {
                if(Origin.X < Position.X)
                {
                    Position -= Velocity;
                }
                else
                {
                    Position-= Velocity;
                }
            }
            else if(Origin.Y != Position.Y)
            {
                if(Origin.Y < Position.Y)
                {
                    Position -= Velocity;
                }
                else
                {
                    Position -= Velocity;
                }
            }
            else
            {
                Position = Origin;
                isActive = true;
                Velocity = Vector2.Zero;
            }
        }

        public void Update()
        {
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Size.X, (int)Sprite.Size.Y);
            Sprite.UpdatePosition(Position);
            if (DetectLink() && isActive)
            {
                Active();
            }
            else
            {
                Inactive();
            }
        }

        private bool DetectLink()
        {
            return Math.Abs(Position.X - Game.Link.Position.X) < 96 && Math.Abs(Position.Y - Game.Link.Position.Y) < 96;
        }

    }
}
