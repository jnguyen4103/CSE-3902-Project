
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
        private string directionSpriteName;
        private Game1 Game;
        int DamageTimer = 0;
        int DamageDelay = 90;


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



        public DamagedLink(Link _link, Game1 game, string oldSpriteName, Link.LinkDirection _direction)
        {
            decoratedLink = _link;
            Game = game;
            Sprite = _link.SpriteLink;
            LinkSM = _link.LinkSM;
            directionSpriteName = oldSpriteName;
            Direction = _direction;
            Sprite.FPS = 8;
        }

        public void TakeDamage(int damage)
        {

        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }

        public void Update()
        {

            SpriteLink.Update(Link.LinkState.Damaged, Direction);
            DamageTimer++;

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
            Sprite.FPS = 8;
            Game.Link.StateMachine.IdleState();
        }

        public Link.LinkState GetState()
        { 
            return Link.LinkState.Damaged;
        }
    }
}