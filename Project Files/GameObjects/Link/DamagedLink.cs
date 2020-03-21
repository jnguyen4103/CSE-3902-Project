/* Contributors
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class DamagedLink: ILink
    {
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public LinkSprite Sprite { get; set; }
        public LinkStateMachine StateMachine { get; set; }
        public States.LinkState State { get; set; } = States.LinkState.Damaged;
        public States.Direction Direction { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public float BaseSpeed { get; set; } = 1f;
        public bool CanMove { get; set; } = false;


        private Game1 Game;
        private Link decoratedLink;
        private Vector2 KnockbackVelocity = new Vector2(0, 0);
        private int DamagedTimer = 0;
        private int StunnedDuration = 24;
        private int DamagedDuration = 120;
        private bool HitBoundary = false;

        public DamagedLink(Game1 game, Link link)
        {
            Game = game;
            decoratedLink = link;
            Sprite = decoratedLink.Sprite;
            Sprite.Colour *= 0.75f;
            Hitbox = decoratedLink.Hitbox;
            Position = decoratedLink.Position;
            Direction = decoratedLink.Direction;
            FindVelocity();

        }

        public void TakeDamage(States.Direction directionHit, int damage)
        {
            // Cannot take damage in damaged state
        }

        public void Attack()
        {
            // Cannot attack while damaged
        }

        public void SecondaryAttack(string attackName)
        {
            // I dunno, maybe sometime later
        }

        public void ChangeDirection(States.Direction direction)
        {
            if (Direction != direction)
            {
                switch (direction)
                {
                    case (States.Direction.Up):
                        Direction = States.Direction.Up;
                        Sprite.ChangeSpriteAnimation("DamagedUp");
                        break;
                    case (States.Direction.Down):
                        Direction = States.Direction.Down;
                        Sprite.ChangeSpriteAnimation("DamagedDown");
                        break;
                    case (States.Direction.Left):
                        Direction = States.Direction.Left;
                        Sprite.ChangeSpriteAnimation("DamagedLeft");
                        break;
                    case (States.Direction.Right):
                        Direction = States.Direction.Right;
                        Sprite.ChangeSpriteAnimation("DamagedRight");
                        break;
                }
                Direction = direction;
            }
        }


        public void StopLink()
        {
            if (HitBoundary)
            {
                // Change state to idle
            }
            HitBoundary = true;
        }

        public void Update()
        {
            Hitbox = new Rectangle((int)Position.X + 2, (int)Position.Y + 4, (int)Sprite.Size.X - 4, (int)Sprite.Size.Y - 4);
            DamagedTimer++;
            if(DamagedTimer <= StunnedDuration && !HitBoundary) { Knockback(); }

            if (DamagedTimer >= DamagedDuration) { RemoveDecorator(); }
        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }

        private void Knockback()
        {
            if (State != States.LinkState.Idle)
            {
                if (DamagedTimer < StunnedDuration)
                {
                    Position += KnockbackVelocity;
                    decoratedLink.Position = Position;
                    Sprite.UpdatePosition(Position);
                }
                else if (DamagedTimer == StunnedDuration)
                {
                    State = States.LinkState.Idle;
                    BaseSpeed = 0.64f;
                    CanMove = true;
                }
            }
        }

        private void FindVelocity()
        {
            switch (Direction)
            {
                case (States.Direction.Up):
                    KnockbackVelocity.Y = 1.5f * BaseSpeed;
                    break;

                case (States.Direction.Down):
                    KnockbackVelocity.Y = -1.5f * BaseSpeed;
                    break;

                case (States.Direction.Left):
                    KnockbackVelocity.X = 1.5f * BaseSpeed;
                    break;

                case (States.Direction.Right):
                    KnockbackVelocity.X = -1.5f * BaseSpeed;
                    break;
            }
        }

        private void RemoveDecorator()
        {
            decoratedLink.State = States.LinkState.Idle;
            decoratedLink.ResetSprite();

            decoratedLink.ChangeDirection(Direction);
            decoratedLink.Position = Position;
            decoratedLink.Hitbox = Hitbox;
            Game.Link = decoratedLink;
            Game.Link.CanMove = true;

        }
    }
}
