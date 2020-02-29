﻿using System;

namespace Sprint03
{
    public class AttackingLink : ILink
    {
        private IEffect SwordAttack;
        private IEffect SwordBeam;
        private LinkSprite Sprite;
        private LinkStateMachine LinkSM;
        private Link decoratedLink;
        private Link.LinkDirection Direction;
        private string directionSpriteName;
        private Game1 Game;
        private int LifeSpan;
        private int AttackTimer = 0;


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

        public AttackingLink(Link _link, Game1 game, string oldSpriteName, Link.LinkDirection _direction)
        {
            decoratedLink = _link;
            Game = game;
            Sprite = _link.SpriteLink;
            LinkSM = _link.LinkSM;
            directionSpriteName = oldSpriteName;
            Direction = _direction;
            Sprite.FPS = 16;
            LifeSpan = (60/Sprite.FPS) * 4;
            SwordAttack = new SwordEffect(_link.SpriteLink, game, _direction, game.EffectSpriteSheet, game.spriteBatch);
            SwordBeam = new SwordBeamEffect(_link.SpriteLink, game, _direction, game.EffectSpriteSheet, game.spriteBatch);

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
            SpriteLink.Update(Link.LinkState.Attacking, Direction);

            AttackTimer++;
            if (AttackTimer == (LifeSpan/3))
            {
                SwordAttack.CreateEffect();
            }
            else if (AttackTimer == (LifeSpan/2))
            {
                Sprite.ChangeSpriteAnimation(directionSpriteName);

            }
            else if (AttackTimer >= LifeSpan)
            {
                if (decoratedLink.hitpoints == decoratedLink.maxHP)
                {
                    SwordBeam.CreateEffect();
                }
                RemoveDecorator();
            }
        }

        public void RemoveDecorator()
        {
            AttackTimer = 0;
            SpriteLink.Update(Link.LinkState.Idle, Direction);
            Game.Link = decoratedLink;
            Sprite.FPS = 8;
            Game.Link.StateMachine.IdleState();
        }

        public Link.LinkState GetState()
        {
            return Link.LinkState.Attacking;
        }
    }
}