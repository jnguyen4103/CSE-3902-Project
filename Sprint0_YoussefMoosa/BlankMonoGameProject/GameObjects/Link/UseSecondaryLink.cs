
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{

    public class UseSecondaryLink : ILink
    {

        private LinkSprite Sprite;
        private LinkStateMachine LinkSM;
        private Link decoratedLink;
        private Link.LinkDirection Direction;
        private IEffect AttackEffect;
        private string directionSpriteName;
        private Game1 Game;
        int Lifespan; 
        int UseSecondaryTimer;


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



        public UseSecondaryLink(Link _link, Game1 game, string oldSpriteName, Link.LinkDirection _direction, IEffect effect)
        {
            decoratedLink = _link;
            Game = game;
            Sprite = _link.SpriteLink;
            LinkSM = _link.LinkSM;
            AttackEffect = effect;
            directionSpriteName = oldSpriteName;
            Direction = _direction;
            Sprite.FPS = 8;
            Lifespan = (60 / Sprite.FPS) * 2;
            Sprite.ChangeSpriteAnimation("Effect" + decoratedLink.LinkSM.GetDirection());

            if (effect != null)
            {
                AttackEffect.CreateEffect();
            }
        }

        public void TakeDamage(int damage, int direction)
        {
            // You can be damaged while attacking
            // So it removes the attacking decorator and adds the damaged one
            RemoveDecorator();
            Game.Link.TakeDamage(damage, direction);

        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }

        public void Update()
        {
            SpriteLink.Update(Link.LinkState.UsingSecondary, Direction);
            UseSecondaryTimer++;

            if (UseSecondaryTimer >= Lifespan)
            {
                RemoveDecorator();
            }
        }

        public void RemoveDecorator()
        {
            UseSecondaryTimer = 0;
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