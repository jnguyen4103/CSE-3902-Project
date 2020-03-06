
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{

    public class DamagedLink : ILink
    {

        private LinkSprite Sprite;
        private LinkStateMachine LinkSM;
        private Link decoratedLink;
        private Link.LinkDirection Direction;
        private Link.LinkDirection PushbackDirection;
        private string directionSpriteName;
        private Game1 Game;
        int DamageTimer = 0;
        int DamageDelay = 30;


        LinkStateMachine ILink.StateMachine
        {
            get { return LinkSM; }
            set { }
        }

        public LinkSprite SpriteLink
        {
            get { return Sprite; }
            set { }
        }

        public int HP { get => decoratedLink.hitpoints; set => decoratedLink.hitpoints = value; }
        public int MaxHP { get => decoratedLink.maxHP; set => decoratedLink.maxHP = value; }



        public DamagedLink(Link _link, Game1 game, string oldSpriteName, Link.LinkDirection _direction, int directionDamaged)
        {
            decoratedLink = _link;
            Game = game;
            Sprite = _link.SpriteLink;
            LinkSM = _link.LinkSM;
            directionSpriteName = oldSpriteName;
            Direction = _direction;
            Sprite.FPS = 8;
            Sprite.BaseSpeed = 1.5f;
            PushbackDirection = GetPushbackDirection(directionDamaged);
            DamageTimer = 0;
        }

        public void TakeDamage(int damage, int direction)
        {

        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }

        public void Update()
        {
            DamageTimer++;
            SpriteLink.Update(Link.LinkState.Damaged, PushbackDirection);

            if (DamageTimer > DamageDelay)
            {
                RemoveDecorator();
            }
        }

        public void RemoveDecorator()
        {
            DamageTimer = 0;
            SpriteLink.Update(Link.LinkState.Idle, Direction);
            Game.Link = decoratedLink;
            SpriteLink.BaseSpeed = 1f;
            Sprite.FPS = 8;
            Game.Link.StateMachine.IdleState();
        }

        public Link.LinkState GetState()
        { 
            return Link.LinkState.Damaged;
        }

        private Link.LinkDirection GetPushbackDirection(int directionDamaged)
        {
            Link.LinkDirection dir;

            switch (directionDamaged)
            {
                case (0):
                    dir = Link.LinkDirection.Down;
                    break;
                case (1):
                    dir = Link.LinkDirection.Up;
                    break;
                case (2):
                    dir = Link.LinkDirection.Right;
                    break;
                case (3):
                    dir = Link.LinkDirection.Left;
                    break;
                default:
                    dir = Link.LinkDirection.Down;
                    break;
            }

            return dir;
        }

        public void StopLink()
        {
            throw new NotImplementedException();
        }
    }
}