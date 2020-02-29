using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint03
{
    public class LinkSprite : Sprite
    {
        Link.LinkDirection Direction;
        Link.LinkState State;


        public LinkSprite(Game1 game, String name, Texture2D texture, Vector2 spawn, SpriteBatch batch)
        {
            Game = game;
            Batch = batch;
            Name = name;
            Layer = 0.25f;
            Size = game.SFactory.LinkSprites[name].Item2;
            Position = spawn;
            Texture = texture;
            CurrentFrame = 0;
            TotalFrames = game.SFactory.LinkSprites[name].Item3;
            FPS = 60;
            ChangeSpriteAnimation(name);
            BaseSpeed = 1f;
        }

        public override void ChangeSpriteAnimation(string newSpriteName)
        {
            if (Name != newSpriteName) { CurrentFrame = 0; }
            Name = newSpriteName;
            Tuple<Rectangle, Vector2, int> NewInfo = Game.SFactory.LinkSprites[newSpriteName];
            DrawWindow = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            AnimationWindow = new Rectangle(NewInfo.Item1.X, NewInfo.Item1.Y * CurrentFrame, (int)NewInfo.Item2.X, (int)NewInfo.Item2.Y);
            TotalFrames = NewInfo.Item3;
        }


        public override void Animate()
        {
            if (State != Link.LinkState.Idle)
            {
                GameFrame++;
                if ((60 / FPS) <= GameFrame)
                {
                    GameFrame = 0;
                    CurrentFrame++;
                    if (CurrentFrame >= TotalFrames)
                    {
                        CurrentFrame = 0;
                    }
                }
            }
        }

        public void Update(Link.LinkState _state, Link.LinkDirection _direction)
        {
            State = _state;
            Direction = _direction;
        }

        public override void Move()
        {
            if (State == Link.LinkState.Moving || State == Link.LinkState.Damaged)
            {
                switch (Direction)
                {
                    case (Link.LinkDirection.Down):
                        Position.Y += BaseSpeed;
                        break;
                    case (Link.LinkDirection.Up):
                        Position.Y -= BaseSpeed;
                        break;
                    case (Link.LinkDirection.Left):
                        Position.X -= BaseSpeed;
                        break;
                    case (Link.LinkDirection.Right):
                        Position.X += BaseSpeed;
                        break;
                    default:
                        break;
                }
            }

        }

        public override void DrawSprite()
        {
            Animate();
            Move();
            DrawWindow.X = (int)Position.X;
            DrawWindow.Y = (int)Position.Y;
            AnimationWindow.Y = (int)(CurrentFrame * Size.Y);
            Batch.Draw(Texture, DrawWindow, AnimationWindow, Color.White, Rotation, Origin, SpriteEffect, Layer);
        }

    }
}
