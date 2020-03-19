using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class SecondaryAttackLink : ILink
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
        private int Timer = 0;
        private int StopDuration = 15;

        public SecondaryAttackLink(Game1 game, Link link)
        {
            Game = game;
            decoratedLink = link;
            Sprite = decoratedLink.Sprite;
            Hitbox = decoratedLink.Hitbox;
            Position = decoratedLink.Position;
            Direction = decoratedLink.Direction;
            HP = decoratedLink.HP;
            MaxHP = decoratedLink.MaxHP;

        }

        public void TakeDamage(States.Direction directionHit, int damage)
        {
            RemoveDecorator();
            decoratedLink.TakeDamage(directionHit, damage);
        }

        public void Attack()
        {
            // Does nothing
        }

        public void SecondaryAttack(string attackName)
        {
            // Does nothing
        }

        public void ChangeDirection(States.Direction direction)
        {
            // Cannot move
        }


        public void StopLink()
        {
            // Link cannot move thus he does not need to stop
        }

        public void Update()
        {
            Hitbox = new Rectangle((int)Position.X + 2, (int)Position.Y + 4, (int)Sprite.Size.X - 4, (int)Sprite.Size.Y - 4);
            Timer++;
            if (Timer >= StopDuration)
            {
                RemoveDecorator();
            }
        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }



        private void RemoveDecorator()
        {
            decoratedLink.State = States.LinkState.Idle;
            decoratedLink.Position = Position;
            decoratedLink.Hitbox = Hitbox;
            Game.Link = decoratedLink;
            decoratedLink.ResetSprite();
            decoratedLink.CanMove = true;
        }
    }
}
